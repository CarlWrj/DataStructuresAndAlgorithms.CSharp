using Algorithms.DataCompressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Test.DataCompression
{
    public class HuffmanCodeTest
    {
        [Fact]
        public void Test()
        {
            var huffmanCode = new HuffmanCode();
            var data = "sdfs0f80a38234lkdfjsd90f809a939329493284923842sdfajsdfoisdfjsdf";
            var compressedData = huffmanCode.Compress(data);
            var decompressedData = huffmanCode.Decompress(compressedData);

            Assert.Equal(data, decompressedData);
        }
    }
}
