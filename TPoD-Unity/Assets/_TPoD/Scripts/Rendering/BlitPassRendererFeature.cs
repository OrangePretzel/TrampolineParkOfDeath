using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TPoD.Rendering
{
	public class BlitPassRendererFeature : ScriptableRendererFeature
	{
		public enum Target
		{
			CameraTarget,
			RenderTexture
		}

		[System.Serializable]
		public class BlitSettings
		{
			public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

			public Material BlitMaterial = null;
			public int BlitMaterialPassIndex = -1;
			public Target Destination = Target.CameraTarget;
			public string TextureID = "_BlitPassTexture";
		}

		// The casing of 'settings' matters for Unity
		public BlitSettings settings = new BlitSettings();

		private BlitPass _featurePass;

		private RenderTargetHandle m_RenderTextureHandle;

		// Called once at initialization
		public override void Create()
		{
			var passIndex = settings.BlitMaterial != null ? settings.BlitMaterial.passCount - 1 : 1;
			settings.BlitMaterialPassIndex = Mathf.Clamp(settings.BlitMaterialPassIndex, -1, passIndex);
			_featurePass = new BlitPass(settings.Event, settings.BlitMaterial, settings.BlitMaterialPassIndex, name);
			m_RenderTextureHandle.Init(settings.TextureID);
		}

		// Called every frame, once per camera
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			var sourceTarget = renderer.cameraColorTarget;
			var destinationTarget = (settings.Destination == Target.CameraTarget)
				? RenderTargetHandle.CameraTarget
				: m_RenderTextureHandle;

			if (settings.BlitMaterial == null)
			{
				Debug.LogWarningFormat("Missing Blit Material. {0} blit pass will not execute. Check for missing reference in the assigned renderer.", GetType().Name);
				return;
			}

			// Setup and add the render pass
			_featurePass.Setup(sourceTarget, destinationTarget);
			renderer.EnqueuePass(_featurePass);
		}
	}
}
