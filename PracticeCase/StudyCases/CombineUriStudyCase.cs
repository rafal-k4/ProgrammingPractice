using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PracticeCase.StudyCases
{
    class CombineUriStudyCase: IStudy
    {
        public void Execute()
        {
            var asd3 = @"http://asd.com/SaleAdmin/";
            var asd4 = @"api/path/path";

            var uri = new Uri(new Uri(asd3, UriKind.RelativeOrAbsolute), asd4);

            Uri.TryCreate(new Uri(asd3), asd4, out var result4);

            var pathToUri = Path.Combine(asd3, asd4).Replace("\\", "/");

            Console.WriteLine(uri.AbsolutePath + $"{Environment.NewLine}" + uri.AbsoluteUri);
            Console.WriteLine(result4.AbsolutePath + $"{Environment.NewLine}" + result4.AbsoluteUri);
            Console.WriteLine(pathToUri + $"{Environment.NewLine}");
            Console.WriteLine(CombineUri(asd3, asd4) + $"{Environment.NewLine}" + CombineUri(asd3, asd4).AbsoluteUri);

            Console.ReadLine();
        }

        public static Uri CombineUri(string baseUri, string relativeUri)
        {
            var baseUriWithRequiredSlashAtEnd = baseUri.Trim().Substring(baseUri.Length - 1) != "/"
                ? baseUri.Trim() + "/"
                : baseUri;
            var relativePathWithoutSlashAtBeginning = relativeUri.TrimStart('/');

            return new Uri(new Uri(baseUriWithRequiredSlashAtEnd), relativePathWithoutSlashAtBeginning);
        }
    }
}
