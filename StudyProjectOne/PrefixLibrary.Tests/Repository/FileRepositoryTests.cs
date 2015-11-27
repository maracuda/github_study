using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PrefixLibrary.Models;
using PrefixLibrary.Repository;

namespace PrefixLibrary.Tests.Repository
{
    [TestFixture]
    public class FileRepositoryTests
    {
        private FileRepository _repository;
        string repo_path = AppDomain.CurrentDomain.BaseDirectory + "../TestInput.txt";

        [SetUp]
        public void Init()
        {

            _repository = new FileRepository(repo_path);
        }

        [Test()]
        public void IsPrefixStringUnique_NotExistedString_Success()
        {
            _repository.Delete("1");

            var result = _repository.IsPrefixStringUnique("123.123.123.0/30");

            Assert.That(result, Is.EqualTo(""));
        }

        [Test()]
        public void IsPrefixStringUnique_ExistedString_Fail()
        {
            _repository.Delete("1");
            _repository.Add(new PrefixView("1", "123.123.123.0/30"));

            var result = _repository.IsPrefixStringUnique("123.123.123.0/30");

            Assert.That(result, Is.EqualTo("Такой префикс существует!"));
        }
        [Test()]
        public void IsPrefixIdUnique_NotExistedPrefix_Success()
        {
            _repository.Delete("1");

            var result = _repository.IsPrefixIdUnique("1");

            Assert.That(result, Is.EqualTo(""));
        }

        [Test()]
        public void IsPrefixIdUnique_ExistedPrefix_Fail()
        {
            _repository.Delete("1");
            _repository.Add(new PrefixView("1", "123.123.123.0/30"));

            var result = _repository.IsPrefixIdUnique("1");
            _repository.Delete("1");

            Assert.That(result, Is.EqualTo("Префикс с таким Id существует!"));
        }

        [Test()]
        public void Add_NewPrefix_PrefixAdded()
        {
            _repository.Add(new PrefixView("1", "123.123.123.0/30"));

            var result = _repository.Read("1");
            _repository.Delete("1");

            Assert.That(result.ToString(), Is.EqualTo(new PrefixView("1", "123.123.123.0/30").ToString()));
        }

        [Test()]
        public void Remove_ExistedPrefix_PrefixRemoved()
        {
            _repository.Add(new PrefixView("1", "123.123.123.0/30"));

            _repository.Delete("1");

            Assert.That(false, Is.EqualTo(_repository.ReadAll().Any(t=> t.Id == "1")));
        }

        [Test()]
        public void Update_ExistedPrefix_PrefixUpdated()
        {
            _repository.Add(new PrefixView("1", "123.123.123.0/30"));

            _repository.Update("1", new PrefixView("1", "10.0.0.0/8"));
            var result = _repository.Read("1");
            _repository.Delete("1");

            Assert.That(result.ToString(), Is.EqualTo(new PrefixView("1", "10.0.0.0/8").ToString()));

        }

    }
}