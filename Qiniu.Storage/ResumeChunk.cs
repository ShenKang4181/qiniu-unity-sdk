namespace Qiniu.Storage
{
	public class ResumeChunk
	{
		private static int N = 131072;

		public static int GetChunkSize(ChunkUnit cu)
		{
			return (int)cu * N;
		}

		public static ChunkUnit GetChunkUnit(int chunkSize)
		{
			if (chunkSize < 131072 || chunkSize > 4194304)
			{
				return ChunkUnit.U2048K;
			}
			int num = chunkSize / N;
			return (num == 1) ? ChunkUnit.U128K : ((num < 4) ? ChunkUnit.U256K : ((num < 8) ? ChunkUnit.U512K : ((num < 16) ? ChunkUnit.U1024K : ((num >= 32) ? ChunkUnit.U4096K : ChunkUnit.U2048K))));
		}
	}
}
