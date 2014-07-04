ThePirateSharp
==============

ThePirateSharp is a .NET API to make torrent searches in The Pirate Bay website.

### Download

You can clone this repository or download the static files in the [Releases](https://github.com/ggondim/thepiratesharp/releases/latest)

You may also download to your Visual Studio web project via [NuGet](https://www.nuget.org/packages/thepiratesharp/):
```
install-package thepiratesharp
```

## How to use

Just add a reference for ThePirateSharp to your project and use the `Tpb` class to make a search.

Here is a good example of the basic `Tpb` usage:

```
IEnumerable<Torrent> torrents = Tpb.Search(new Query("frozen"));
```

### Torrent Class

The `Tpb.Search()` method returns a collection of Torrent objects, which have the following structure:

* **Name**: the name.
* **Magnet**: the magnet URI.
* **File**: a link to a .torrent file if the torrent have it. `default: string.Empty` 
* **Uploaded**: the date of upload, with an unformatted TPB style.
* **Size**: a string containing a decimal number and a byte unit describing the size (i.e: "1.23 GiB").
* **SizeBytes**: the computed bytes in a decimal type for the `Size` property.
* **Uled**: the nickname of the creator.
* **Seeds**: the number of seeds.
* **Leechers**: the number of leechers.
* **CategoryParent**: the parent category (Audio, Video, Application, Games, Porn, Other).
* **Category**: the child category (i.e: "Movies").
* **Comments**: the comment count if the torrent have it. `default: 0`
* **HasCoverImage**: a flag indicating if it has a cover image. `default: false`
* **IsTrusted**: a flag indicating if the creator user is a trusted user. `default: false`
* **IsVip**: a flag indicating if the creator user is a VIP user. `default: false`

### Advanced query parameters with the Query Class

You can use the following query parameters if you want to:

* **Order**: a QueryOrder enum item.
* **Category**: a TPB's category ID. To use valid IDs, use the `TorrentCategory` members.
* **Page**: the zeri-based index of search page.
* **Term**: the search term.

### More examples

```
// Query the term "frozen", starting at the fourth page (index = 3).
IEnumerable<Torrent> torrents = Tpb.Search(new Query("frozen", 3));

// Query the term "windows", starting at the third page, having the parent category equals to "Application".
IEnumerable<Torrent> torrents = Tpb.Search(new Query("windows", 2, TorrentCategory.AllApplication));

// Query the term "skyrim", starting at the first page, having the child category equals to "PC", and ordering by seeds.
IEnumerable<Torrent> torrents = Tpb.Search(new Query("skyrim", 0, TorrentCategory.Games.PC, QueryOrder.BySeeds));
```
