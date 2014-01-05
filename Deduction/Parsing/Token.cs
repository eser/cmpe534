
namespace Deduction.Parsing
{
    public class Token
    {
        // fields
        protected readonly string content;
        protected readonly RegistryMember registryMember;

        // constructors
        public Token(string content, RegistryMember registryMember)
        {
            this.content = content;
            this.registryMember = registryMember;
        }

        // properties
        public string Content
        {
            get
            {
                return this.content;
            }
        }

        public RegistryMember RegistryMember
        {
            get
            {
                return this.registryMember;
            }
        }
    }
}
