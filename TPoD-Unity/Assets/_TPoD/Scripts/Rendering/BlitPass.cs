using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TPoD.Rendering
{
	/// <summary>
	/// Copy the given color buffer to the given destination color buffer.
	///
	/// You can use this pass to copy a color buffer to the destination,
	/// so you can use it later in rendering. For example, you can copy
	/// the opaque texture to use it for distortion effects.
	/// </summary>
	public class BlitPass : ScriptableRenderPass
	{
		private Material _blitMaterial;
		// If -1 (default), draws all passes in the material. Otherwise, draws given pass only.
		private int _blitShaderPassIndex = 0;
		private FilterMode _filterMode;

		private RenderTargetIdentifier _sourceTarget;
		private RenderTargetHandle _destinationTarget;

		private RenderTargetHandle m_TemporaryColorTexture;
		private readonly string _profilerTag;
		private readonly ProfilingSampler _profilingSampler;

		public BlitPass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag)
		{
			this.renderPassEvent = renderPassEvent;
			_blitMaterial = blitMaterial;
			_blitShaderPassIndex = blitShaderPassIndex;
			_filterMode = FilterMode.Bilinear;

			// Setup profiling variables
			_profilerTag = tag;
			_profilingSampler = new ProfilingSampler(_profilerTag);

			m_TemporaryColorTexture.Init("_TemporaryColorTexture");
		}

		/// <summary>
		/// Configure the pass with the source and destination to execute on.
		/// </summary>
		/// <param name="source">Source Render Target</param>
		/// <param name="destination">Destination Render Target</param>
		public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
		{
			_sourceTarget = source;
			_destinationTarget = destination;
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmdBuffer = CommandBufferPool.Get(_profilerTag);

			RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
			opaqueDesc.depthBufferBits = 0;

			using (new ProfilingScope(cmdBuffer, _profilingSampler))
			{
				cmdBuffer.ExecuteAndClear(context);

				// Can't read and write to same color target, create a temp render target to blit. 
				if (_destinationTarget == RenderTargetHandle.CameraTarget)
				{
					cmdBuffer.GetTemporaryRT(m_TemporaryColorTexture.id, opaqueDesc, _filterMode);
					Blit(cmdBuffer, _sourceTarget, m_TemporaryColorTexture.Identifier(), _blitMaterial, _blitShaderPassIndex);
					Blit(cmdBuffer, m_TemporaryColorTexture.Identifier(), _sourceTarget);
				}
				else
				{
					Blit(cmdBuffer, _sourceTarget, _destinationTarget.Identifier(), _blitMaterial, _blitShaderPassIndex);
				}
			}

			cmdBuffer.ExecuteAndClear(context);
			CommandBufferPool.Release(cmdBuffer);
		}

		public override void FrameCleanup(CommandBuffer cmd)
		{
			if (_destinationTarget == RenderTargetHandle.CameraTarget)
				cmd.ReleaseTemporaryRT(m_TemporaryColorTexture.id);
		}
	}
}
