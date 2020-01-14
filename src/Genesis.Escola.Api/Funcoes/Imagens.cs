using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.Funcoes
{
    public class Imagens
    {

        public static KeyValuePair<int, string> UploadArquivo(byte[] arquivo, string imgNome, string caminho, string caminhoAmbiente, bool existe = true)
        {

            if (arquivo.Length == 0)
            {
                return new KeyValuePair<int, string>(1, "Forneça uma imagem!");
            }
            var imageDataByteArray = arquivo;
            string webRoot = caminhoAmbiente + caminho;

            if (!Directory.Exists(webRoot))
            {
                Directory.CreateDirectory(webRoot);
            }

            imgNome += ".png";

            var filePath = Path.Combine(webRoot, imgNome);
            if (existe)
            {
                if (System.IO.File.Exists(filePath))
                {
                    return new KeyValuePair<int, string>(1, "Já existe um arquivo com este nome!");
                }
            }
            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            imgNome = caminho + imgNome;

            return new KeyValuePair<int, string>(0, imgNome);
        }
    }

    public class Pdfs
    {

        public static KeyValuePair<int, string> UploadArquivo(byte[] arquivo, string pdfNome, string caminho, string caminhoAmbiente, bool existe = true)
        {

            if (arquivo.Length == 0)
            {
                return new KeyValuePair<int, string>(1, "Forneça um Arquivo PDF!");
            }
            var imageDataByteArray = arquivo;
            string webRoot = caminhoAmbiente + caminho;

            if (!Directory.Exists(webRoot))
            {
                Directory.CreateDirectory(webRoot);
            }

            pdfNome += ".pdf";

            var filePath = Path.Combine(webRoot, pdfNome);
            if (existe)
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            pdfNome = caminho + pdfNome;

            return new KeyValuePair<int, string>(0, pdfNome);
        }
    }
}
