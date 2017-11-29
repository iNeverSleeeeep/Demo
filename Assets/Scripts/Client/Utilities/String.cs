
namespace Demo.Utils
{
    class StringBuilder
    {
        static System.Text.StringBuilder instance = null;
        public static System.Text.StringBuilder Instance
        {
            get
            {
                if (instance == null)
                    instance = new System.Text.StringBuilder();
                return instance;
            }
        }
    }
}

