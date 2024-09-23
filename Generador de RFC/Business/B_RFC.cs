using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace Business
{
    public class B_RFC
    {

        /*------------------------------ NUEVOS REGISTROS ------------------------------*/

        List<char> vocals = new List<char> { 'A', 'E', 'I', 'O', 'U' }; //Lista de vocales para comparar existencia
        List<char> forbiddenVocals = new List<char> { 'Á', 'É', 'Í', 'Ó', 'Ú' }; //Lista de vocales prohibidas (con acento)
        List<string> forbiddenFirstNames = new List<string> { "MARIA", "MA", "MA.", "JOSE", "J", "J." }; //Lista de primeros nombres prohibidos
        Dictionary<string, string> forbiddenWords = new Dictionary<string, string> //Diccionario de palabras prohibidas formadas en la parte alfabetica del RFC
        {
            { "BUEI","BUEX" },{ "CACA","CACX" },{ "CAGA","CAGX" },{ "CAKA","CAKX" },{ "COGE","COGX" },
            { "COJE","COJX" },{ "COJO","COJX" },{ "FETO","FETX" },{ "JOTO","JOTX" },{ "KACO","KACX" },
            { "KOJO","KOJX" },{ "KULO","KULX" },{ "MAMO","MAMX" },{ "MEAS","MEAX" },{ "MION","MIOX" },
            { "MULA","MULX" },{ "PEDO","PEDX" },{ "PUTA","PUTX" },{ "QULO","QULX" },{ "BUEY","BUEX" },
            { "CACO","CACX" },{ "CAGO","CAGX" },{ "CAKO","CAKX" },{ "COJA","COJX" },{ "COJI","COJX" },
            { "CULO","CULX" },{ "GUEY","GUEX" },{ "KACA","KACX" },{ "KAGA","KAGX" },{ "KOGE","KOGX" },
            { "KAKA","KAKX" },{ "MAME","MAMX" },{ "MEAR","MEAX" },{ "MEON","MEOX" },{ "MOCO","MOCX" },
            { "PEDA","PEDX" },{ "PENE","PENX" },{ "PUTO","PUTX" },{ "RATA","RATX" }
        };

        public void Create(E_RFC user)
        {
            D_RFC userCreator = new D_RFC();

            try
            {
                //Primero: generar RFC
                string rfc = GenerateRFC(user);

                //Segundo: Verificar si está repetido el usuario
                bool isRepeated = userCreator.GetRepeatedUser(user, rfc);
                if (isRepeated == false)
                {
                    //Tercero: Crear el usuario
                    userCreator.CreateUser(user, rfc);
                }
                else
                {
                    rfc = rfc + "*";
                    userCreator.CreateUser(user, rfc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GenerateRFC(E_RFC user)
        {
            string paternalSurname = user.PaternalSurname.ToUpper(); //Desde aqui convertimos todo a mayusculas para facilitar la manipulación de datos (comparaciones y asginaciones)
            string maternalSurname = user.MaternalSurname;
            string name = user.Name.ToUpper();
            string birthDate = user.BirthDate.ToString("yy-MM-dd");

            //Comprobaciones antes de tratar los datos
            paternalSurname = BlankSpacesValidation(paternalSurname); //Si hay espacios en blanco al inicio o al final, los eliminará.
            if (maternalSurname != null)
            {
                maternalSurname = BlankSpacesValidation(maternalSurname);
            }
            name = BlankSpacesValidation(name);
            maternalSurname = NullValidation(maternalSurname); //Si no es null, convertimos a mayuculas.

            //TRATAR LOS DATOS PARA GENERAR EL RFC (Un metodo para sacar cada parte del RFC)
            string firstPosition = pSurnameLetters(paternalSurname); //Primer apellido: primer letra y primer vocal interna.
            string secondPosition = mSurnameLetter(maternalSurname); //Segundo apellido: primer letra
            string thirdPosition = NameLetters(name); //Si son dos nombres, partir la cadena en subcadenas, split por espacios
            string fourthPosition = BirthdateNumbers(birthDate);

            // Evaluar la parte alfabetica del RFC a fin de evitar palabras inconvenientes
            string toClean = $"{firstPosition}{secondPosition}{thirdPosition}";
            string cleaned = Cleaner(toClean);

            //Armar RFC completo
            string rfc = $"{cleaned}{fourthPosition}";

            return rfc;
        }

        public string pSurnameLetters(string paternalSurname)
        {
            // Lista vacía que contendrá los caracteres seleccionados
            List<char> selected = new List<char>();

            //NOTA: Sí, se pudieron utilizar dos variables y luego concatenarlas, pero hay que practicar manejo de List y Arrays
            char tempChar;

            char[] allCharacters = paternalSurname.ToCharArray(); //Arreglo que contiene el apellido dividido en caracteres
            for (int i = 0; i < allCharacters.Length; i++)
            {
                if (i == 0) //Si es la primer iteración...
                {
                    tempChar = allCharacters[i];

                    if (tempChar == 'Ñ') //Si el caracter en primera posicion es 'ñ', sustituimos por 'x'
                    {
                        tempChar = 'X';
                        selected.Add(tempChar);
                        continue;
                    }
                    else
                    {
                        selected.Add(allCharacters[i]); //...agrega esa primer letra a la lista de seleccionados
                        continue;
                    }
                }
                else if (forbiddenVocals.Contains(allCharacters[i])) //No permite vocales con acento
                {
                    throw new Exception("Ningún campo puede contener letras con acentos o diéresis");
                }
                else if (vocals.Contains(allCharacters[i])) //Primer coincidencia de vocal en el apellido excluyendo la primer letra.
                {
                    selected.Add(allCharacters[i]);
                    break; //Si llegamos a este punto ya tenemos la primer letra y la primer vocal, detenemos el For.
                }
                else
                {
                    continue;
                }
            }

            //Si no encontramos ninguna vocal interna en el apellido, agregamos una x en la segunda posición
            if (selected.Count == 1)
            {
                selected.Add('X');
            }

            string selectedCharacters = string.Join("", selected); //Las letras seleccionadas, las juntamos en un string

            return selectedCharacters;
        }

        public string mSurnameLetter(string maternalSurname)
        {
            if (maternalSurname != null)
            {
                char[] allCharacters = maternalSurname.ToCharArray();
                string selected = allCharacters[0].ToString(); //Solo nos interesa la primer letra independientemente de si es vocal o consonante
                AccentValidation(Convert.ToChar(selected));
                selected = EnieValidation(selected);

                return selected;
            }
            else //Si no hay segundo apellido (es null), retornamos "x".
            {
                return "X";
            }
        }

        public string NameLetters(string name)
        {
            string[] names = name.Split(' ');

            if (names.Length == 1) //Si solo hay un nombre, toma la primer letra del mismo independientemente del nombre que tenga (no aplica la lista forbiddenFirstNames)
            {
                char[] allCharacters = name.ToCharArray(); //Ya sabemos que solo es un nombre, utilizamos la variable pasada por parametro del método
                string selected = allCharacters[0].ToString();

                AccentValidation(Convert.ToChar(selected));
                selected = EnieValidation(selected);

                return selected;
            }
            else //Si hay mas de un nombre, revisamos que el primer nombre no esté en la lista de forbiddenFirstNames
            {
                if (forbiddenFirstNames.Contains(names[0])) //Si el primer nombre no está permitido...
                {
                    char[] allCharacters = names[1].ToCharArray(); //...tomamos el segundo nombre (independientemente de si hay mas de dos)
                    string selected = allCharacters[0].ToString();
                    AccentValidation(Convert.ToChar(selected));
                    selected = EnieValidation(selected);

                    return selected;
                }
                else
                {
                    char[] allCharacters = names[0].ToCharArray(); //...tomamos el segundo nombre (independientemente de si hay mas de dos)
                    string selected = allCharacters[0].ToString();
                    AccentValidation(Convert.ToChar(selected));
                    selected = EnieValidation(selected);

                    return selected;
                }
            }
        }

        public string Cleaner(string toClean) //Metodo para sustituir ("limpiar") el RFC de palabras inconvenientes
        {
            if (forbiddenWords.ContainsKey(toClean)) //Si existe alguna key en el diccionario de palabras prohibidas, que coincida con el RFC...
            {
                return forbiddenWords[toClean]; //...se sustituye por el valor asociado a dicha llave (la palabra correcta)
            }
            else
            {
                return toClean; //Si no hay nada que cambiar, devuelve la misma entrada
            }
        }

        public string BirthdateNumbers(string birthDate)
        {
            return birthDate.Replace("-", ""); //Elimina los guiones del formato de la fecha
        }

        public void AccentValidation(char selected)
        {
            if (forbiddenVocals.Contains(selected)) //Si se encuentran coincidencias de letras con acentos, retorna error
            {
                throw new Exception("Ningún campo puede contener letras con acentos o diéresis");
            }
        }

        public string EnieValidation(string selected) //Si una de las letras seleccioadas para el RFC es una "ñ", la sustituye por una "x"
        {
            if (selected == "Ñ")
            {
                selected = "X";
            }
            return selected;
        }

        public string BlankSpacesValidation(string field)
        {
            return field.Trim();
        }

        public string NullValidation(string maternalSurname)
        {
            if (maternalSurname != null)
            {
                return maternalSurname.ToUpper();
            }
            else
            {
                return maternalSurname;
            }
        }

        /*------------------------------ RETORNO DE ULTIMO REGISTRO ------------------------------*/

        public string GetLastRFC()
        {
            D_RFC getRfc = new D_RFC();

            try
            {
                string rfc = getRfc.GetLastRFC();
                return rfc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*------------------------------ RETORNO DE TODOS LOS REGISTROS ------------------------------*/

        public List<E_RFC> GetAllUsers()
        {
            D_RFC getall = new D_RFC();

            try
            {
                List<E_RFC> users = getall.GetAllUsers();
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CountUsers()
        {
            D_RFC counter = new D_RFC();

            try
            {
                int count = counter.CountUsers();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*------------------------------ RETORNO DE UN SOLO REGISTRO POR ID ------------------------------*/

        public E_RFC GetByID(int id)
        {
            D_RFC getuser = new D_RFC();
            try
            {
                E_RFC user = getuser.GetByID(id);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*------------------------------ ELIMINAR UN REGISTRO POR ID ------------------------------*/
        public void DeleteById(int id)
        {
            D_RFC deleter = new D_RFC();

            try
            {
                deleter.DeleteById(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*------------------------------ ELIMINAR UN REGISTRO POR ID ------------------------------*/
        public void Edit(E_RFC user)
        {
            D_RFC userEditor = new D_RFC();

            try
            {
                string rfc = GenerateRFC(user);

                bool isRepeated = userEditor.GetRepeatedUser(user, rfc);

                if (isRepeated == false)
                {
                    userEditor.Edit(user, rfc);
                }
                else
                {
                    rfc = rfc + "*";
                    userEditor.Edit(user, rfc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /*------------------------------ BUSCAR REGISTROS POR COINCIDENCIA DE TEXTO ------------------------------*/

        public List<E_RFC> Search(string text)
        {
            D_RFC searcher = new D_RFC();
            List<E_RFC> users = new List<E_RFC>();

            text = text.Trim();

            try
            {
                users = searcher.Search(text);
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}