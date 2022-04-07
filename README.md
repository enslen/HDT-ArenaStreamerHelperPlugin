# HDT-ArenaStreamerHelperPlugin
A simple Hearthstone Deck Tracker plugin that exports current arena run and overall arena stats to text files for display during streaming.  Features include:

- Current deck stats
- Running list of recent runs (added to as runs are completed, sort options in settings)
- Advanced stats (current class and all classes)
  - Average
  - Best Run
  - Total Runs
  - Wins/Losses
  - Win Rate

# How To Install

**1.** Download the compiled plugin above or click this [link](https://github.com/enslen/HDT-ArenaStreamerHelperPlugin/blob/main/ArenaStreamerHelper.zip).

**2.** Refer to the instructions here: https://github.com/HearthSim/Hearthstone-Deck-Tracker/wiki/Available-Plugins

# How To Use

**1.** Enable the plugin if you haven't already in Hearthstone Deck Tracker (covered in the wiki link above).

**2.** Ensure relevant options are checked (probably just do all of them).

**3.** Set the text file locations and advanced stats location.

**4.** Set whether you want new runs to appear at the top or bottom of your recent runs list.

**5.** Setup the sources in your favorite streaming software like OBS. I suggest you choose a [monospaced font](https://en.wikipedia.org/wiki/Monospaced_font) when you setup your text sources. This keeps everything aligned properly.

**6.** Profit!

# Troubleshooting

The plugin is stable for me. It may not be stable for you. I have had a couple of other people use it before so please double check your settings before reporting bugs/issues. Make sure you have the latest version of HDT installed and make sure you have appropriate permissions to any files or folders that you are writing to.

This plugin does not include archived runs. Additionally, if you never archive your runs such that you might have hundreds or thousands of runs showing in HDT, I don't know if you will have any issues (performance or otherwise).  Keep that in mind when using this and consider archiving runs if you do have performance issues or random crashing.

# Future

I realize I could also do something similar for ranked or other play modes. I primarily play arena so I wasn't thinking about that when I created this. If there is a lot of interest, I may expand the features. The most obvious feature arena wise would be to calculate your current best 30 run average for the current month and output that to separate text files. But if you can code, feel free to do so yourself!

**DISCLAIMER:** I am not a developer/programmer by trade but I'd like to believe I am a step above script kiddie. :) As such, the code found within is by no means optimal. It probably could be worse though! If you are a developer/programmer, please be nice. Thanks.