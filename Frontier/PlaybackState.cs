// -----------------------------------------------------------------------
// <copyright file="PlaybackState.cs" company="John Lynch">
//   This file is licensed under the MIT license
//   Copyright (c) 2020 John Lynch
// </copyright>
// -----------------------------------------------------------------------

namespace Frontier {
	using PioneerApi;

	public class PlaybackState {
		public string Title { get; set; }
		public string Artist { get; set; }
		public string Album { get; set; }

		public ApiClient.PlayInfo PlayStatus { get; set; }
	}
}