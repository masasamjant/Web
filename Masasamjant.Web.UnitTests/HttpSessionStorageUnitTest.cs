using Masasamjant.Web.Stubs;

namespace Masasamjant.Web
{
    [TestClass]
    public class HttpSessionStorageUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var session = new SessionStub();
            var context = CreateHttpContext(session);
            var storage1 = new HttpSessionStorage(session);
            var storage2 = new HttpSessionStorage(context);
            Assert.IsNotNull(storage1);
            Assert.IsNotNull(storage2);
            storage1.SetString("key", "value");
            Assert.AreEqual("value", storage2.GetString("key"));
        }

        [TestMethod]
        public void Test_Clear()
        {
            var session = new SessionStub();
            var expectedsessionIdentifier = session.Id;
            var context = CreateHttpContext(session);
            var storage = new HttpSessionStorage(context);
            string? actualSessionIdentifier = null;
            storage.Cleared += (s, e) => {
                actualSessionIdentifier = e.SessionIdentifier;
            };
            storage.SetString("key", "value");
            var x = storage.GetString("key");
            Assert.AreEqual("value", x);
            storage.Clear();
            x = storage.GetString("key");
            Assert.IsNull(x);
            Assert.AreEqual(expectedsessionIdentifier, actualSessionIdentifier);
        }

        [TestMethod]
        public void Test_GetString()
        {
            var context = CreateHttpContext(new SessionStub());
            var storage = new HttpSessionStorage(context);
            var value = storage.GetString("key");
            Assert.IsNull(value);
            storage.SetString("key", "value");
            value = storage.GetString("key");
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void Test_Remove()
        {
            var context = CreateHttpContext(new SessionStub());
            var storage = new HttpSessionStorage(context);
            storage.SetString("1", "1");
            storage.SetString("2", "2");
            var value = storage.GetString("2");
            Assert.AreEqual("2", value);
            storage.Remove("2");
            value = storage.GetString("2");
            Assert.IsNull(value);
            Assert.AreEqual("1", storage.GetString("1"));
        }

        [TestMethod]
        public void Test_SetString()
        {
            var context = CreateHttpContext(new SessionStub());
            var storage = new HttpSessionStorage(context);
            storage.SetString("1", "1");
            Assert.AreEqual("1", storage.GetString("1"));
            storage.SetString("1", "2");
            Assert.AreEqual("2", storage.GetString("1"));
        }

        [TestMethod]
        public void Test_GetSessionIdentifier()
        {
            var session = new SessionStub();
            var context = CreateHttpContext(session);
            var storage = new HttpSessionStorage(context);
            Assert.AreEqual(session.Id, storage.GetSessionIdentifier());
        }
    }
}
