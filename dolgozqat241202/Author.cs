using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolgozqat241202
{
    internal class Author
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Author(string fullName)
        {
            if (string.IsNullOrEmpty(fullName) || !fullName.Contains(" "))
            {
                throw new ArgumentException("A névnek tartalmaznia kell egy vezeték- és keresztnevet.");
            }

            var names = fullName.Split(' ');

            if (names.Length != 2 || names[0].Length < 3 || names[1].Length < 3 || names[0].Length > 32 || names[1].Length > 32)
            {
                throw new ArgumentException("A kereszt- és vezetékneveknek 3-32 karakter hosszúaknak kell lenniük.");
            }

            Id = Guid.NewGuid();
            FirstName = names[0];
            LastName = names[1];
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
