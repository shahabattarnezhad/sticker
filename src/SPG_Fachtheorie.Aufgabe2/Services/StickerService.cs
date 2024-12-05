using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Services
{
    public record SaleStatistics(string StickerTypeName, decimal TotalRevenue);

    public class StickerService
    {
        private readonly StickerContext _db;

        public StickerService(StickerContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Checks the permission for a scanned numberplate.
        /// </summary>
        public bool HasPermission
            (string numberplate, DateTime dateTime, VehicleType carType)
        {
            var result = 
                _db.Stickers.Any(s =>
                    s.Numberplate == numberplate
                    &&
                    s.StickerType.VehicleType == carType
                    &&
                    dateTime >= s.ValidFrom
                    &&
                    dateTime <= s.ValidFrom.AddDays(s.StickerType.DaysValid)
                );

            return result;
        }

        /// <summary>
        /// Calculates the total revenue for every sticker type sold in a specific year.
        /// Use PurchaseDate.Year to get the year of purchase.
        /// Hint: To use Sum() you have to cast Price to double.
        ///       After that you have to cast the sum back to decimal.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<SaleStatistics> CalcSaleStatistics(int year)
        {
            var stickers = 
                _db.Stickers.Where(s => s.PurchaseDate.Year == year)
                            .GroupBy(s => s.StickerType.Name)
                            .ToList();

            var result = 
                stickers.Select(x => new SaleStatistics
            (
                x.Key, 
                (decimal)x.Sum(s => (double)s.Price)
            )).ToList();

            return result;
        }
    }
}