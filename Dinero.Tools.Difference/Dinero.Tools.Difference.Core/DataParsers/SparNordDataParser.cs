﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using FileHelpers;

namespace Dinero.Tools.Difference.Core.DataParsers
{
    public class SparNordDataParser : IDataParser
    {
        public IEnumerable<EntryModel> Parse(string data)
        {
            var engine      = new FileHelperEngine<SparNordEntry>();
            var parseResult = engine.ReadString(data);
            var result      = new List<EntryModel>();

            for (var index = 0; index < parseResult.Length; index++)
            {
                var nordeaEntry = parseResult[index];
                var entryModel = new EntryModel()
                {
                    Index   = index,
                    Amount  = nordeaEntry.Amount,
                    Date    = nordeaEntry.Date,
                    Text    = nordeaEntry.Text,
                    Saldo   = nordeaEntry.Saldo
                };
                result.Add(entryModel);
            }

            return result;
        }

        [DelimitedRecord(";"), IgnoreEmptyLines(), IgnoreFirst()]
        private class SparNordEntry
        {
            [FieldOrder(1)]
            [FieldConverter(ConverterKind.Date, @"dd/MM/yyyy")]
            public DateTime Date;

            [FieldOrder(2)]
            public string Text;

            [FieldConverter(ConverterKind.Decimal, ",")]
            [FieldOrder(3)]
            public decimal Amount;

            [FieldOrder(4)]
            [FieldConverter(ConverterKind.Decimal, ",")]
            public decimal Saldo;
        }
    }
}
