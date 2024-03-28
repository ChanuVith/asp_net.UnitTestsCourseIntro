using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsCourse.Tests
{
    public static class WorldsDumbestFunctionTests
    {
        //Naming convention - ClassName_MethodName_ExpectedResult
        public static void WorldsDumbestFunction_ReturnsPickachuIfZero_ReturnString()
        {
            try
            {
                //Arrange - Go get your variables, whatever you need, your classes, go get functions
                int num = 0;
                WorldsDumbestFunction worldsDumbest = new WorldsDumbestFunction();

                //Act - Execute this function.
                string result = worldsDumbest.ReturnsPickachuIfZero(num);

                //Assert - Whatever is returned, is it what you want.
                if (result == "Pikachu")
                {
                    Console.WriteLine("Passed: worldsDumbestFunction.ReturnsPickachuIfZero_ReturnString");
                }
                else
                {
                    Console.WriteLine("Failded: worldsDumbestFunction.ReturnsPickachuIfZero_ReturnString");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
