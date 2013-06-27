﻿// Copyright 2013 Jon Skeet. All rights reserved. Use of this source code is governed by the Apache License 2.0, as found in the LICENSE.txt file.
using System;

namespace LinqToOperators
{
    class IncrementDecrement
    {
        static void Main()
        {
            var source = new[] { "Arithmetic", "-", "+", "*", "/" }.Evil();

            Console.WriteLine(source);
            Console.WriteLine(++source);
            Console.WriteLine(--source);
        }
    }
}
