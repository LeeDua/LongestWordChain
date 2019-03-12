[DllImport(@"F:\\我爱学习学习爱我\\SoftwareEngineeringCourse\\InterlopTest\\InterplotTest\\Debug\\MyCppDll.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            EntryPoint = "TestDllAdd",
            ExactSpelling = false,
            SetLastError = true)]
        public static extern int TestDllAdd(int a, int b);



LIBRARY MyCppDll
EXPORTS
TestDllAdd @1



