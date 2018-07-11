
namespace _URLTester.Test
{
    public interface IURLTest<T>
    {

        bool LoadCSV();
        bool TestLinks();
        bool OutPutResults();
        void OutPutErrorMessages();
    }

}
