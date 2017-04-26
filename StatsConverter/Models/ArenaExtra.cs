﻿using System;
using System.Collections.Generic;
using System.Linq;
using HDT.Plugins.Common.Models;
using HDT.Plugins.Common.Enums;
using HDT.Plugins.Common.Services;

namespace HDT.Plugins.StatsConverter.Models
{
	public class ArenaExtra
	{
		private IDataRepository _data;

		public string Name { get; private set; }
		public PlayerClass PlayerClass { get; private set; }
		public List<string> Cards { get; private set; }
		public int Gold { get; private set; }
		public int Dust { get; private set; }
		public int CardReward { get; private set; }
		public int Packs { get; private set; }
		public string Payment { get; private set; }
		public DateTime LastPlayed { get; private set; }
		public int Win { get; private set; }
		public int Loss { get; private set; }

		public ArenaExtra(IDataRepository data, Deck deck, List<Game> stats = null)
		{
			_data = data;
			Name = deck.Name;
			PlayerClass = deck.Class;
			LastPlayed = deck.LastPlayed;
			Cards = deck.Cards.Select(x => x.ToString()).ToList();
			var run = RunRecord(deck, stats);
			Win = run.Item1;
			Loss = run.Item2;

			if (deck.ArenaReward != null)
			{
				Gold = deck.ArenaReward.Gold;
				Dust = deck.ArenaReward.Dust;
				CardReward = deck.ArenaReward.Cards.Where(x => x != null).Count();
				Packs = deck.ArenaReward.Packs.Where(x => !string.IsNullOrWhiteSpace(x)).Count();
				Payment = deck.ArenaReward.PaymentMethod.ToString();
			}
		}

		private Tuple<int, int> RunRecord(Deck d, List<Game> filtered)
		{
			var relevant = _data.GetAllGamesWithDeck(d.Id);
			if (filtered != null)
			{
				relevant = relevant.Intersect(filtered).ToList();
			}
			return new Tuple<int, int>(
				relevant.Count(g => g.Result == GameResult.WIN),
				relevant.Count(g => g.Result == GameResult.LOSS));
		}
	}
}