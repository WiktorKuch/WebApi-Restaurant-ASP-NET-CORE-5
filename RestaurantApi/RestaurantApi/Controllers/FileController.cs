using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Any;
using System.Collections.Generic;
using System.IO;

namespace RestaurantAPI.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : ControllerBase
    {

        
        [HttpGet]
        [ResponseCache(Duration =1200,VaryByQueryKeys = new[] {"fileName"})]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            
            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

            var fileExists = System.IO.File.Exists(filePath);
            if(!fileExists)
            {
                return NotFound();
            }//jeśli plik istnieje to musimy zwrócić jego zawartość jako tablice bajtów 


            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(fileName, out var contentType); // info jaki jest typ pliku 



            var fileContents = System.IO.File.ReadAllBytes(filePath); //aby załadować plik do pamięci -parametr ścieżka 
            return File(fileContents,contentType , fileName); // 1.zawartość pliku 2. typ contentu 3. nazwa pliku z jaką klient go pobierze z serwera 
            
        }

        [HttpPost]
        public ActionResult UpLoad([FromForm] IFormFile file)
        {

            if(file!=null && file.Length >0) 
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/PrivateFiles/{fileName}"; // ścieżka pod którą dany plik będzie zapisany 

                using(var stream = new FileStream(fullPath, FileMode.Create)) //zapisanie pliku
                {
                    file.CopyTo(stream);
                }

                return Ok();
            }
            return BadRequest();

        }


    }
}
