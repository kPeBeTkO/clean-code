﻿using System;
using Markdown;
using NUnit.Framework;

namespace Markdown_Tests
{
    [TestFixture]
    public class MdToHtml_Translation_Tests
    {
        [Test]
        public void Test()
        {
            var text = "Текст, _окруженный с двух сторон_ одинарными символами подчерка, должен помещаться в HTML-тег <em>";
            Console.WriteLine(Md.TranslateToHtml(text));
        }
        
        [TestCase("Текст, _окруженный с двух сторон_ одинарными символами подчерка, должен помещаться в HTML-тег <em>",
            "<p>Текст, <em>окруженный с двух сторон</em> одинарными символами подчерка, должен помещаться в HTML-тег <em></p>")]
        [TestCase("__Выделенный двумя символами текст__ должен становиться полужирным с помощью тега <strong>",
            "<p><strong>Выделенный двумя символами текст</strong> должен становиться полужирным с помощью тега <strong></p>")]
        [TestCase("# Заголовок __с _разными_ символами__",
            "<h1>Заголовок <strong>с <em>разными</em> символами</strong></h1>")]
        public void Render_Test(string input, string expectedRender)
        {
            var actualRender = Md.TranslateToHtml(input);
            Assert.AreEqual(expectedRender, actualRender);
        }
        
        [TestCase("\\_Вот это\\_, не должно выделиться тегом <em>",
            "<p>_Вот это_, не должно выделиться тегом <em></p>")]
        [TestCase("\\_ \\_\\_",
            "<p>_ __</p>")]
        [TestCase("сл\\эш",
            "<p>сл\\эш</p>")]
        [TestCase("\\\\",
            "<p>\\</p>")]
        [TestCase("\\\\_Крусив_",
            "<p>\\<em>Крусив</em></p>")]
        [TestCase("\\\\__Жирный__",
            "<p>\\<strong>Жирный</strong></p>")]
        public void ShieldingSpecification_Test(string input, string expectedRender)
        {
            var actualRender = Md.TranslateToHtml(input);
            Assert.AreEqual(expectedRender, actualRender);
        }
        
        [TestCase("Внутри __двойного выделения _одинарное_ тоже__ работает.",
            "<p>Внутри <strong>двойного выделения <em>одинарное</em> тоже</strong> работает.</p>")]
        [TestCase("Но не наоборот — внутри _одинарного __двойное__ не_ работает.",
            "<p>Но не наоборот — внутри <em>одинарного __двойное__ не</em> работает.</p>")]
        [TestCase("Подчерки внутри текста c цифрами_12_3 не считаются выделением и должны оставаться символами подчерка.",
            "<p>Подчерки внутри текста c цифрами_12_3 не считаются выделением и должны оставаться символами подчерка.</p>")]
        [TestCase("Однако выделять часть слова они могут: и в _нач_але, и в сер_еди_не, и в кон_це._",
            "<p>Однако выделять часть слова они могут: и в <em>нач</em>але, и в сер<em>еди</em>не, и в кон<em>це.</em></p>")]
        [TestCase("В то же время выделение в ра_зных сл_овах не работает.",
            "<p>В то же время выделение в ра_зных сл_овах не работает.</p>")]
        [TestCase("__Непарные_ символы в рамках одного абзаца не считаются выделением.",
            "<p>__Непарные_ символы в рамках одного абзаца не считаются выделением.</p>")]
        [TestCase("эти_ подчерки_ не считаются выделением",
            "<p>эти_ подчерки_ не считаются выделением</p>")]
        [TestCase("эти _подчерки _не считаются_ окончанием выделения",
            "<p>эти _подчерки _не считаются_ окончанием выделения</p>")]
        [TestCase("В случае __пересечения _двойных__ и одинарных_ подчерков ни один из них не считается выделением.",
            "<p>В случае __пересечения _двойных__ и одинарных_ подчерков ни один из них не считается выделением.</p>")]
        [TestCase("Если внутри подчерков пустая строка ____, то они остаются символами подчерка.",
            "<p>Если внутри подчерков пустая строка ____, то они остаются символами подчерка.</p>")]
        public void TagInteraction_Test(string input, string expectedRender)
        {
            var actualRender = Md.TranslateToHtml(input);
            Assert.AreEqual(expectedRender, actualRender);
        }
        
        [TestCase("[Contribution guidelines for this project](docs/CONTRIBUTING.md)",
            "<p><a href=\"docs/CONTRIBUTING.md\">Contribution guidelines for this project</a></p>")]
        [TestCase("![This is an image](https://myoctocat.com/assets/images/base-octocat.svg)",
            "<p><img src=\"https://myoctocat.com/assets/images/base-octocat.svg\" alt=\"This is an image\"></p>")]
        public void ImageAndLink_Test(string input, string expectedRender)
        {
            var actualRender = Md.TranslateToHtml(input);
            Assert.AreEqual(expectedRender, actualRender);
        }
    }
}