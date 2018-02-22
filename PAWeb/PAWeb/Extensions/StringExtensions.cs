using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAWeb.Extensions
{
    public class StringExtensions
    {
        public int GetIdFromFileName(IFormFile image)
        {
            string fileNameToSplit = image.FileName;
            var splittedString = fileNameToSplit.Split(".jpg");

            int id = int.Parse(splittedString[0]);

            return id;
        }
    }
}
