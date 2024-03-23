using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DesafioBackEnd.Controllers
{
    [Produces("application/json", new string[] { })]
    public class PalindromoController : Controller
    {
        [Route("manipulacao-string")]
        [HttpPost]
        public ActionResult TestaSePalindromo([FromBody] string entrada)
        {
            try
            {
                //dynamic obj = JsonConvert.DeserializeObject(entrada);
                //string texto = obj.texto;
                Palindromo palindromo = new Palindromo();
                if (ValidaPalindromo(entrada))
                {
                    palindromo.palindromo = true;
                }
                else
                {
                    palindromo.palindromo = false;
                }
                //JsonConversao jsonconv = new JsonConversao();
                //string saida = jsonconv.ConverteObjectParaJSon(palindromo);
                return Ok(ContaLetras(entrada));
            }
            catch
            {
                return BadRequest("string de entrada é inválida");
            }
        }

        public class Palindromo
        {
            public bool palindromo {  get; set; }
        }

        public class JsonConversao
        {
            public string ConverteObjectParaJSon<T>(T obj)
            {
                try
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    MemoryStream ms = new MemoryStream();
                    ser.WriteObject(ms, obj);
                    string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                    return jsonString;
                }
                catch
                {
                    throw;
                }
            }
        }

        public List<string> ContaLetras(string entrada)
        {
            var lista = new List<string>();
            var listaCaracteres = new List<caractere>();
            char caractere;
            for (int i = 0; i < entrada.Length; i++)
            {
                caractere = entrada[i];
                foreach (caractere caractere1 in listaCaracteres)
                {
                    if (caractere != caractere1.id)
                    {
                        int quantidade = 0;
                        foreach (char caractere2 in entrada)
                        {
                            if (caractere == caractere2)
                                quantidade++;
                        }
                        caractere1.qtde = quantidade;
                        listaCaracteres.Add(caractere1);
                    }
                }
            }
            foreach (caractere caractere1 in listaCaracteres)
            {
                lista.Add(caractere1.id + ":" + caractere1.qtde);
            }
            return lista;
        }

        public class caractere
        {
            public char id { get; set; }
            public int qtde { get; set; }
        }

        public bool ValidaPalindromo(string entrada)
        {
            int i = 0;
            int j = entrada.Length - 1;
            while (i < j)
            {
                if (entrada[i] != entrada[j])
                {
                    return false;
                }
                i++;
                j--;
            }
        return true;
        }
    }
}
