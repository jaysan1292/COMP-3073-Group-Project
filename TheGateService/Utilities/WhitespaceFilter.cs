// Project: TheGateService
// Filename: WhitespaceFilter.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TheGateService.Utilities {
    internal class WhitespaceFilter : Stream {
        private static readonly Regex BlankLines = new Regex(@"^\s+$", RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly Stream _stream;

        public WhitespaceFilter(Stream stream) {
            _stream = stream;
        }

        public override void Write(byte[] buffer, int offset, int count) {
            var data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            var content = Encoding.Default.GetString(buffer);
            content = BlankLines.Replace(content, "");
            var output = Encoding.Default.GetBytes(content);
            _stream.Write(output, 0, output.GetLength(0));
        }

        public override void Flush() {
            _stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value) {
            _stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count) {
            return _stream.Read(buffer, offset, count);
        }

        public override bool CanRead { get { return _stream.CanRead; } }

        public override bool CanSeek { get { return _stream.CanSeek; } }

        public override bool CanWrite { get { return _stream.CanWrite; } }

        public override long Length { get { return _stream.Length; } }

        public override long Position { get; set; }
    }
}
