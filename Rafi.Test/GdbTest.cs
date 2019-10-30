using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rafi.Test
{
    public class GdbTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitialSequence()
        {
            var list = new List<(string, string)>
            {
                ("$qSupported:multiprocess+;swbreak+;hwbreak+;qRelocInsn+;fork-events+;vfork-events+;exec-events+;vContSupported+;QThreadEvents+;no-resumed+#df", "+$PacketSize=1000#f1"),
                ("$vMustReplyEmpty#3a", "+$#00"),
                ("$Hgp0.0#ad", "+$OK#9a"),
                ("$qTStatus#49", "+$#00"),
                ("$?#3f", "+$S05#b8"),
                ("$qfThreadInfo#bb", "+$mp01.01#cd"),
                ("$qsThreadInfo#c8", "+$l#6c"),
                ("$qAttached:1#fa", "+$1#31"),
                ("$Hc-1#09", "+$OK#9a"),
                ("$qfThreadInfo#bb", "+$mp01.01#cd"),
                ("$qsThreadInfo#c8", "+$l#6c"),
            };

            foreach (var e in list)
            {
                var request = e.Item1;

                var req = new MemoryStream(Encoding.UTF8.GetBytes(request));
                var res = new MemoryStream();

                using (var reader = new StreamReader(req))
                using (var writer = new StreamWriter(res))
                {
                    GdbSession.Process(reader, writer);

                    var response = Encoding.UTF8.GetString(res.ToArray());

                    Assert.AreEqual(e.Item2, response);
                }
            }
        }
    }
}