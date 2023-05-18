namespace DwapiCentral.Shared.Custom;

 public static class CustomExtentions
    {

        /// <summary>
        /// compare strings insensitive
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsSameAs(this string value, string other)
        {
            if (value == null)
                return false;
            return value.Trim().ToLower() == other.Trim().ToLower();
        }

        public static string HasToEndWith(this string value, string end)
        {
            if (value == null)
                return string.Empty;

            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static string HasToStartWith(this string value, string start)
        {
            if (value == null)
                return string.Empty;

            return value.StartsWith(start) ? value : $"{start}{value}";
        }

        public static string ToUnixStyle(this string value)
        {
            if (value == null)
                return string.Empty;

            return value.Replace(@"\", @"/");
        }

        public static string ToOsStyle(this string value)
        {
            if (value == null)
                return string.Empty;

            if (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
                return value.Replace(@"\", @"/");

            return value.Replace(@"/", @"\");
        }

        public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> enumerable,
            int chunkSize)
        {
            if (chunkSize < 1) throw new ArgumentException("chunkSize must be positive");

            using (var e = enumerable.GetEnumerator())
                while (e.MoveNext())
                {
                    var remaining = chunkSize; // elements remaining in the current chunk
                    var innerMoveNext = new Func<bool>(() => --remaining > 0 && e.MoveNext());

                    yield return e.GetChunk(innerMoveNext);
                    while (innerMoveNext())
                    {
                        /* discard elements skipped by inner iterator */
                    }
                }
        }

        private static IEnumerable<T> GetChunk<T>(this IEnumerator<T> e,
            Func<bool> innerMoveNext)
        {
            do yield return e.Current;
            while (innerMoveNext());
        }

        public static string ToNamespace(this string value)
        {
            if (value == null)
                return string.Empty;

            return value.ToOsStyle()
                .Replace(@"/", @".")
                .Replace(@"\", @".");
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }