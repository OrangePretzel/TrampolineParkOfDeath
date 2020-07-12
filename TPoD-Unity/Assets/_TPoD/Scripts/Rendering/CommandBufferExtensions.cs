using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace TPoD.Rendering
{
	public static class CommandBufferExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ExecuteAndClear(this CommandBuffer cmdBuffer, ScriptableRenderContext context)
		{
			context.ExecuteCommandBuffer(cmdBuffer);
			cmdBuffer.Clear();
		}
	}
}
