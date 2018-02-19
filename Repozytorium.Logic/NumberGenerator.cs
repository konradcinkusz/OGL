using Microsoft.AspNet.Identity.EntityFramework;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repozytorium.Logic
{
    public class NumberGenerator
    {
        const int numberPoolCount = 10000000;
        private OglContext _entities;

        public OglContext _context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public NumberGenerator()
        {
            _context = new OglContext();
        }

        /// <summary>
        /// Generuje losowy numer paczki z puli i zapisuje go na baze
        /// </summary>
        /// <returns>numer paczki</returns>
        public virtual int Generate()
        {
            int generateNumber = -1;
            do
            {
                generateNumber = StockNumberFromSortedRange(numberPoolCount);
            } while (BinarySearch(new Tuple<int, int>(0, numberPoolCount), generateNumber) != -1);

            if (generateNumber == -1)
                throw new Exception("Something went wrong with application or empty pool");
            _context.NumeryWykorzystane.Add(new Model.UsedNumber { Used = generateNumber });
            _context.SaveChanges();

            return generateNumber;
        }
        protected virtual int BinarySearch(Tuple<int, int> range, int searchElement)
        {
            int leftIndex = 0;//, rightIndex = GetDataFromDB($@"Select Count(*) From {_tableName}");
            int rightIndex = _context.NumeryWykorzystane.Count();
            while (leftIndex < rightIndex)
            {
                int center = (leftIndex + rightIndex) / 2;
                int centerNumber = GetNumberFromDatabaseTableByRowId(center);

                if (centerNumber < searchElement)
                    leftIndex = center + 1;
                else
                    rightIndex = center;
            }
            int leftNumber = GetNumberFromDatabaseTableByRowId(leftIndex);
            return leftNumber == searchElement ? leftIndex - 1 : -1;
        }

        protected int GetNumberFromDatabaseTableByRowId(int rowId) => _context.NumeryWykorzystane.AsQueryable().ToList()[rowId-1].Used;
        protected virtual int StockNumberFromSortedRange(int range) => (new Random()).Next(1, range);
    }
}
