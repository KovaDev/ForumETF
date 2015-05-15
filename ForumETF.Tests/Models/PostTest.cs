using System;
using System.Text;
using System.Collections.Generic;
using ForumETF.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ForumETF.Tests.Models
{
    /// <summary>
    /// Summary description for PostTest
    /// </summary>
    [TestClass]
    public class PostTest
    {
        public PostTest()
        {
            
        }

        [TestMethod]
        public void PostShouldNotBeValidIfTitleEmpty()
        {
            Post post = new Post();

        }



    }
}
