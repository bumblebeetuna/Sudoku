using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Sudoku.Domain.Tests
{
    [TestFixture]
    public class GameFixture
    {
        [Test]
        public void CanGetRow()
        {
            //arrange
            var game = CreateGame(Guid.NewGuid());

            //act
            var row0 = game.GetRow(0).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row1 = game.GetRow(1).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row2 = game.GetRow(2).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row3 = game.GetRow(3).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row4 = game.GetRow(4).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row5 = game.GetRow(5).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row6 = game.GetRow(6).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row7 = game.GetRow(7).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row8 = game.GetRow(8).Select(x => new { x.X, x.Y, x.Value }).ToList();

            //assert
            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 0, 01),new GameMove(Guid.NewGuid(), 1, 0, 02),new GameMove(Guid.NewGuid(), 2, 0, 03),new GameMove(Guid.NewGuid(), 3, 0, 04),new GameMove(Guid.NewGuid(), 4, 0, 05),new GameMove(Guid.NewGuid(), 5, 0, 06),new GameMove(Guid.NewGuid(), 6, 0, 07),new GameMove(Guid.NewGuid(), 7, 0, 08),new GameMove(Guid.NewGuid(), 8, 0, 09),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row0);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 1, 10),new GameMove(Guid.NewGuid(), 1, 1, 11),new GameMove(Guid.NewGuid(), 2, 1, 12),new GameMove(Guid.NewGuid(), 3, 1, 13),new GameMove(Guid.NewGuid(), 4, 1, 14),new GameMove(Guid.NewGuid(), 5, 1, 15),new GameMove(Guid.NewGuid(), 6, 1, 16),new GameMove(Guid.NewGuid(), 7, 1, 17),new GameMove(Guid.NewGuid(), 8, 1, 18),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row1);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 2, 19),new GameMove(Guid.NewGuid(), 1, 2, 20),new GameMove(Guid.NewGuid(), 2, 2, 21),new GameMove(Guid.NewGuid(), 3, 2, 22),new GameMove(Guid.NewGuid(), 4, 2, 23),new GameMove(Guid.NewGuid(), 5, 2, 24),new GameMove(Guid.NewGuid(), 6, 2, 25),new GameMove(Guid.NewGuid(), 7, 2, 26),new GameMove(Guid.NewGuid(), 8, 2, 27),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row2);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 3, 28),new GameMove(Guid.NewGuid(), 1, 3, 29),new GameMove(Guid.NewGuid(), 2, 3, 30),new GameMove(Guid.NewGuid(), 3, 3, 31),new GameMove(Guid.NewGuid(), 4, 3, 32),new GameMove(Guid.NewGuid(), 5, 3, 33),new GameMove(Guid.NewGuid(), 6, 3, 34),new GameMove(Guid.NewGuid(), 7, 3, 35),new GameMove(Guid.NewGuid(), 8, 3, 36),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row3);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 4, 37),new GameMove(Guid.NewGuid(), 1, 4, 38),new GameMove(Guid.NewGuid(), 2, 4, 39),new GameMove(Guid.NewGuid(), 3, 4, 40),new GameMove(Guid.NewGuid(), 4, 4, 41),new GameMove(Guid.NewGuid(), 5, 4, 42),new GameMove(Guid.NewGuid(), 6, 4, 43),new GameMove(Guid.NewGuid(), 7, 4, 44),new GameMove(Guid.NewGuid(), 8, 4, 45),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row4);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 5, 46),new GameMove(Guid.NewGuid(), 1, 5, 47),new GameMove(Guid.NewGuid(), 2, 5, 48),new GameMove(Guid.NewGuid(), 3, 5, 49),new GameMove(Guid.NewGuid(), 4, 5, 50),new GameMove(Guid.NewGuid(), 5, 5, 51),new GameMove(Guid.NewGuid(), 6, 5, 52),new GameMove(Guid.NewGuid(), 7, 5, 53),new GameMove(Guid.NewGuid(), 8, 5, 54),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row5);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 6, 55),new GameMove(Guid.NewGuid(), 1, 6, 56),new GameMove(Guid.NewGuid(), 2, 6, 57),new GameMove(Guid.NewGuid(), 3, 6, 58),new GameMove(Guid.NewGuid(), 4, 6, 59),new GameMove(Guid.NewGuid(), 5, 6, 60),new GameMove(Guid.NewGuid(), 6, 6, 61),new GameMove(Guid.NewGuid(), 7, 6, 62),new GameMove(Guid.NewGuid(), 8, 6, 63),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row6);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 7, 64),new GameMove(Guid.NewGuid(), 1, 7, 65),new GameMove(Guid.NewGuid(), 2, 7, 66),new GameMove(Guid.NewGuid(), 3, 7, 67),new GameMove(Guid.NewGuid(), 4, 7, 68),new GameMove(Guid.NewGuid(), 5, 7, 69),new GameMove(Guid.NewGuid(), 6, 7, 70),new GameMove(Guid.NewGuid(), 7, 7, 71),new GameMove(Guid.NewGuid(), 8, 7, 72),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row7);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 8, 73),new GameMove(Guid.NewGuid(), 1, 8, 74),new GameMove(Guid.NewGuid(), 2, 8, 75),new GameMove(Guid.NewGuid(), 3, 8, 76),new GameMove(Guid.NewGuid(), 4, 8, 77),new GameMove(Guid.NewGuid(), 5, 8, 78),new GameMove(Guid.NewGuid(), 6, 8, 79),new GameMove(Guid.NewGuid(), 7, 8, 80),new GameMove(Guid.NewGuid(), 8, 8, 81),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row8);
        }

        [Test]
        public void CanGetColumn()
        {
            //arrange
            var game = CreateGame(Guid.NewGuid());

            //act
            var row0 = game.GetColumn(0).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row1 = game.GetColumn(1).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row2 = game.GetColumn(2).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row3 = game.GetColumn(3).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row4 = game.GetColumn(4).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row5 = game.GetColumn(5).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row6 = game.GetColumn(6).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row7 = game.GetColumn(7).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row8 = game.GetColumn(8).Select(x => new { x.X, x.Y, x.Value }).ToList();

            //assert
            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 0, 01),
                new GameMove(Guid.NewGuid(), 0, 1, 10),
                new GameMove(Guid.NewGuid(), 0, 2, 19),
                new GameMove(Guid.NewGuid(), 0, 3, 28),
                new GameMove(Guid.NewGuid(), 0, 4, 37),
                new GameMove(Guid.NewGuid(), 0, 5, 46),
                new GameMove(Guid.NewGuid(), 0, 6, 55),
                new GameMove(Guid.NewGuid(), 0, 7, 64),
                new GameMove(Guid.NewGuid(), 0, 8, 73),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row0);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 1, 0, 02),
                new GameMove(Guid.NewGuid(), 1, 1, 11),
                new GameMove(Guid.NewGuid(), 1, 2, 20),
                new GameMove(Guid.NewGuid(), 1, 3, 29),
                new GameMove(Guid.NewGuid(), 1, 4, 38),
                new GameMove(Guid.NewGuid(), 1, 5, 47),
                new GameMove(Guid.NewGuid(), 1, 6, 56),
                new GameMove(Guid.NewGuid(), 1, 7, 65),
                new GameMove(Guid.NewGuid(), 1, 8, 74),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row1);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 2, 0, 03),
                new GameMove(Guid.NewGuid(), 2, 1, 12),
                new GameMove(Guid.NewGuid(), 2, 2, 21),
                new GameMove(Guid.NewGuid(), 2, 3, 30),
                new GameMove(Guid.NewGuid(), 2, 4, 39),
                new GameMove(Guid.NewGuid(), 2, 5, 48),
                new GameMove(Guid.NewGuid(), 2, 6, 57),
                new GameMove(Guid.NewGuid(), 2, 7, 66),
                new GameMove(Guid.NewGuid(), 2, 8, 75),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row2);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 3, 0, 04),
                new GameMove(Guid.NewGuid(), 3, 1, 13),
                new GameMove(Guid.NewGuid(), 3, 2, 22),
                new GameMove(Guid.NewGuid(), 3, 3, 31),
                new GameMove(Guid.NewGuid(), 3, 4, 40),
                new GameMove(Guid.NewGuid(), 3, 5, 49),
                new GameMove(Guid.NewGuid(), 3, 6, 58),
                new GameMove(Guid.NewGuid(), 3, 7, 67),
                new GameMove(Guid.NewGuid(), 3, 8, 76),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row3);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 4, 0, 05),
                new GameMove(Guid.NewGuid(), 4, 1, 14),
                new GameMove(Guid.NewGuid(), 4, 2, 23),
                new GameMove(Guid.NewGuid(), 4, 3, 32),
                new GameMove(Guid.NewGuid(), 4, 4, 41),
                new GameMove(Guid.NewGuid(), 4, 5, 50),
                new GameMove(Guid.NewGuid(), 4, 6, 59),
                new GameMove(Guid.NewGuid(), 4, 7, 68),
                new GameMove(Guid.NewGuid(), 4, 8, 77),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row4);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 5, 0, 06),
                new GameMove(Guid.NewGuid(), 5, 1, 15),
                new GameMove(Guid.NewGuid(), 5, 2, 24),
                new GameMove(Guid.NewGuid(), 5, 3, 33),
                new GameMove(Guid.NewGuid(), 5, 4, 42),
                new GameMove(Guid.NewGuid(), 5, 5, 51),
                new GameMove(Guid.NewGuid(), 5, 6, 60),
                new GameMove(Guid.NewGuid(), 5, 7, 69),
                new GameMove(Guid.NewGuid(), 5, 8, 78),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row5);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 6, 0, 07),
                new GameMove(Guid.NewGuid(), 6, 1, 16),
                new GameMove(Guid.NewGuid(), 6, 2, 25),
                new GameMove(Guid.NewGuid(), 6, 3, 34),
                new GameMove(Guid.NewGuid(), 6, 4, 43),
                new GameMove(Guid.NewGuid(), 6, 5, 52),
                new GameMove(Guid.NewGuid(), 6, 6, 61),
                new GameMove(Guid.NewGuid(), 6, 7, 70),
                new GameMove(Guid.NewGuid(), 6, 8, 79),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row6);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 7, 0, 08),
                new GameMove(Guid.NewGuid(), 7, 1, 17),
                new GameMove(Guid.NewGuid(), 7, 2, 26),
                new GameMove(Guid.NewGuid(), 7, 3, 35),
                new GameMove(Guid.NewGuid(), 7, 4, 44),
                new GameMove(Guid.NewGuid(), 7, 5, 53),
                new GameMove(Guid.NewGuid(), 7, 6, 62),
                new GameMove(Guid.NewGuid(), 7, 7, 71),
                new GameMove(Guid.NewGuid(), 7, 8, 80),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row7);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 8, 0, 09),
                new GameMove(Guid.NewGuid(), 8, 1, 18),
                new GameMove(Guid.NewGuid(), 8, 2, 27),
                new GameMove(Guid.NewGuid(), 8, 3, 36),
                new GameMove(Guid.NewGuid(), 8, 4, 45),
                new GameMove(Guid.NewGuid(), 8, 5, 54),
                new GameMove(Guid.NewGuid(), 8, 6, 63),
                new GameMove(Guid.NewGuid(), 8, 7, 72),
                new GameMove(Guid.NewGuid(), 8, 8, 81),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row8);
        }

        [Test]
        public void CanGetBlock()
        {
            //arrange
            var game = CreateGame(Guid.NewGuid());

            //act
            var row0 = game.GetBlock(0).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row1 = game.GetBlock(1).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row2 = game.GetBlock(2).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row3 = game.GetBlock(3).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row4 = game.GetBlock(4).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row5 = game.GetBlock(5).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row6 = game.GetBlock(6).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row7 = game.GetBlock(7).Select(x => new { x.X, x.Y, x.Value }).ToList();
            var row8 = game.GetBlock(8).Select(x => new { x.X, x.Y, x.Value }).ToList();

            //assert
            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 0, 01),new GameMove(Guid.NewGuid(), 1, 0, 02),new GameMove(Guid.NewGuid(), 2, 0, 03),
                new GameMove(Guid.NewGuid(), 0, 1, 10),new GameMove(Guid.NewGuid(), 1, 1, 11),new GameMove(Guid.NewGuid(), 2, 1, 12),
                new GameMove(Guid.NewGuid(), 0, 2, 19),new GameMove(Guid.NewGuid(), 1, 2, 20),new GameMove(Guid.NewGuid(), 2, 2, 21),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row0);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 3, 0, 04),new GameMove(Guid.NewGuid(), 4, 0, 05),new GameMove(Guid.NewGuid(), 5, 0, 06),
                new GameMove(Guid.NewGuid(), 3, 1, 13),new GameMove(Guid.NewGuid(), 4, 1, 14),new GameMove(Guid.NewGuid(), 5, 1, 15),
                new GameMove(Guid.NewGuid(), 3, 2, 22),new GameMove(Guid.NewGuid(), 4, 2, 23),new GameMove(Guid.NewGuid(), 5, 2, 24),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row1);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 6, 0, 07),new GameMove(Guid.NewGuid(), 7, 0, 08),new GameMove(Guid.NewGuid(), 8, 0, 09),
                new GameMove(Guid.NewGuid(), 6, 1, 16),new GameMove(Guid.NewGuid(), 7, 1, 17),new GameMove(Guid.NewGuid(), 8, 1, 18),
                new GameMove(Guid.NewGuid(), 6, 2, 25),new GameMove(Guid.NewGuid(), 7, 2, 26),new GameMove(Guid.NewGuid(), 8, 2, 27),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row2);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 3, 28),new GameMove(Guid.NewGuid(), 1, 3, 29),new GameMove(Guid.NewGuid(), 2, 3, 30),
                new GameMove(Guid.NewGuid(), 0, 4, 37),new GameMove(Guid.NewGuid(), 1, 4, 38),new GameMove(Guid.NewGuid(), 2, 4, 39),
                new GameMove(Guid.NewGuid(), 0, 5, 46),new GameMove(Guid.NewGuid(), 1, 5, 47),new GameMove(Guid.NewGuid(), 2, 5, 48),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row3);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 3, 3, 31),new GameMove(Guid.NewGuid(), 4, 3, 32),new GameMove(Guid.NewGuid(), 5, 3, 33),
                new GameMove(Guid.NewGuid(), 3, 4, 40),new GameMove(Guid.NewGuid(), 4, 4, 41),new GameMove(Guid.NewGuid(), 5, 4, 42),
                new GameMove(Guid.NewGuid(), 3, 5, 49),new GameMove(Guid.NewGuid(), 4, 5, 50),new GameMove(Guid.NewGuid(), 5, 5, 51),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row4);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 6, 3, 34),new GameMove(Guid.NewGuid(), 7, 3, 35),new GameMove(Guid.NewGuid(), 8, 3, 36),
                new GameMove(Guid.NewGuid(), 6, 4, 43),new GameMove(Guid.NewGuid(), 7, 4, 44),new GameMove(Guid.NewGuid(), 8, 4, 45),
                new GameMove(Guid.NewGuid(), 6, 5, 52),new GameMove(Guid.NewGuid(), 7, 5, 53),new GameMove(Guid.NewGuid(), 8, 5, 54),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row5);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 0, 6, 55),new GameMove(Guid.NewGuid(), 1, 6, 56),new GameMove(Guid.NewGuid(), 2, 6, 57),
                new GameMove(Guid.NewGuid(), 0, 7, 64),new GameMove(Guid.NewGuid(), 1, 7, 65),new GameMove(Guid.NewGuid(), 2, 7, 66),
                new GameMove(Guid.NewGuid(), 0, 8, 73),new GameMove(Guid.NewGuid(), 1, 8, 74),new GameMove(Guid.NewGuid(), 2, 8, 75),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row6);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 3, 6, 58),new GameMove(Guid.NewGuid(), 4, 6, 59),new GameMove(Guid.NewGuid(), 5, 6, 60),
                new GameMove(Guid.NewGuid(), 3, 7, 67),new GameMove(Guid.NewGuid(), 4, 7, 68),new GameMove(Guid.NewGuid(), 5, 7, 69),
                new GameMove(Guid.NewGuid(), 3, 8, 76),new GameMove(Guid.NewGuid(), 4, 8, 77),new GameMove(Guid.NewGuid(), 5, 8, 78),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row7);

            CollectionAssert.AreEqual(new[]{
                new GameMove(Guid.NewGuid(), 6, 6, 61),new GameMove(Guid.NewGuid(), 7, 6, 62),new GameMove(Guid.NewGuid(), 8, 6, 63),
                new GameMove(Guid.NewGuid(), 6, 7, 70),new GameMove(Guid.NewGuid(), 7, 7, 71),new GameMove(Guid.NewGuid(), 8, 7, 72),
                new GameMove(Guid.NewGuid(), 6, 8, 79),new GameMove(Guid.NewGuid(), 7, 8, 80),new GameMove(Guid.NewGuid(), 8, 8, 81),
            }.Select(x => new { x.X, x.Y, x.Value }).ToList(), row8);
        }

        private static Game CreateGame(Guid id)
        {
            return new Game(id, new List<GameMove>
            {
                new GameMove(Guid.NewGuid(), 0, 0, 01),new GameMove(Guid.NewGuid(), 1, 0, 02),new GameMove(Guid.NewGuid(), 2, 0, 03),new GameMove(Guid.NewGuid(), 3, 0, 04),new GameMove(Guid.NewGuid(), 4, 0, 05),new GameMove(Guid.NewGuid(), 5, 0, 06),new GameMove(Guid.NewGuid(), 6, 0, 07),new GameMove(Guid.NewGuid(), 7, 0, 08),new GameMove(Guid.NewGuid(), 8, 0, 09),
                new GameMove(Guid.NewGuid(), 0, 1, 10),new GameMove(Guid.NewGuid(), 1, 1, 11),new GameMove(Guid.NewGuid(), 2, 1, 12),new GameMove(Guid.NewGuid(), 3, 1, 13),new GameMove(Guid.NewGuid(), 4, 1, 14),new GameMove(Guid.NewGuid(), 5, 1, 15),new GameMove(Guid.NewGuid(), 6, 1, 16),new GameMove(Guid.NewGuid(), 7, 1, 17),new GameMove(Guid.NewGuid(), 8, 1, 18),
                new GameMove(Guid.NewGuid(), 0, 2, 19),new GameMove(Guid.NewGuid(), 1, 2, 20),new GameMove(Guid.NewGuid(), 2, 2, 21),new GameMove(Guid.NewGuid(), 3, 2, 22),new GameMove(Guid.NewGuid(), 4, 2, 23),new GameMove(Guid.NewGuid(), 5, 2, 24),new GameMove(Guid.NewGuid(), 6, 2, 25),new GameMove(Guid.NewGuid(), 7, 2, 26),new GameMove(Guid.NewGuid(), 8, 2, 27),
                new GameMove(Guid.NewGuid(), 0, 3, 28),new GameMove(Guid.NewGuid(), 1, 3, 29),new GameMove(Guid.NewGuid(), 2, 3, 30),new GameMove(Guid.NewGuid(), 3, 3, 31),new GameMove(Guid.NewGuid(), 4, 3, 32),new GameMove(Guid.NewGuid(), 5, 3, 33),new GameMove(Guid.NewGuid(), 6, 3, 34),new GameMove(Guid.NewGuid(), 7, 3, 35),new GameMove(Guid.NewGuid(), 8, 3, 36),
                new GameMove(Guid.NewGuid(), 0, 4, 37),new GameMove(Guid.NewGuid(), 1, 4, 38),new GameMove(Guid.NewGuid(), 2, 4, 39),new GameMove(Guid.NewGuid(), 3, 4, 40),new GameMove(Guid.NewGuid(), 4, 4, 41),new GameMove(Guid.NewGuid(), 5, 4, 42),new GameMove(Guid.NewGuid(), 6, 4, 43),new GameMove(Guid.NewGuid(), 7, 4, 44),new GameMove(Guid.NewGuid(), 8, 4, 45),
                new GameMove(Guid.NewGuid(), 0, 5, 46),new GameMove(Guid.NewGuid(), 1, 5, 47),new GameMove(Guid.NewGuid(), 2, 5, 48),new GameMove(Guid.NewGuid(), 3, 5, 49),new GameMove(Guid.NewGuid(), 4, 5, 50),new GameMove(Guid.NewGuid(), 5, 5, 51),new GameMove(Guid.NewGuid(), 6, 5, 52),new GameMove(Guid.NewGuid(), 7, 5, 53),new GameMove(Guid.NewGuid(), 8, 5, 54),
                new GameMove(Guid.NewGuid(), 0, 6, 55),new GameMove(Guid.NewGuid(), 1, 6, 56),new GameMove(Guid.NewGuid(), 2, 6, 57),new GameMove(Guid.NewGuid(), 3, 6, 58),new GameMove(Guid.NewGuid(), 4, 6, 59),new GameMove(Guid.NewGuid(), 5, 6, 60),new GameMove(Guid.NewGuid(), 6, 6, 61),new GameMove(Guid.NewGuid(), 7, 6, 62),new GameMove(Guid.NewGuid(), 8, 6, 63),
                new GameMove(Guid.NewGuid(), 0, 7, 64),new GameMove(Guid.NewGuid(), 1, 7, 65),new GameMove(Guid.NewGuid(), 2, 7, 66),new GameMove(Guid.NewGuid(), 3, 7, 67),new GameMove(Guid.NewGuid(), 4, 7, 68),new GameMove(Guid.NewGuid(), 5, 7, 69),new GameMove(Guid.NewGuid(), 6, 7, 70),new GameMove(Guid.NewGuid(), 7, 7, 71),new GameMove(Guid.NewGuid(), 8, 7, 72),
                new GameMove(Guid.NewGuid(), 0, 8, 73),new GameMove(Guid.NewGuid(), 1, 8, 74),new GameMove(Guid.NewGuid(), 2, 8, 75),new GameMove(Guid.NewGuid(), 3, 8, 76),new GameMove(Guid.NewGuid(), 4, 8, 77),new GameMove(Guid.NewGuid(), 5, 8, 78),new GameMove(Guid.NewGuid(), 6, 8, 79),new GameMove(Guid.NewGuid(), 7, 8, 80),new GameMove(Guid.NewGuid(), 8, 8, 81),
            });
        }
    }
}
