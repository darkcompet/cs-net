namespace Tool.Compet.Core;

using System.Threading;

public class DkAtomicBool {
	/// True: != 0, False: == 0
	private long rawValue;

	public bool value => Interlocked.Read(ref this.rawValue) != 0;

	public bool Set(bool value) {
		if (value) {
			return Interlocked.Or(ref this.rawValue, 1) != 0;
		}
		return Interlocked.And(ref this.rawValue, 0) == 0;
	}
}
