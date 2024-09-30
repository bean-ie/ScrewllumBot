using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Net.Http;
using System.IO.Packaging;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace ScrewllumBot
{
    internal class Program : InteractionModuleBase<SocketInteractionContext>
    {
        private Random random;
        private readonly DiscordSocketClient client;
        private InteractionService interactionService;
        private static IServiceProvider _serviceProvider;

        public Program()
        {
            _serviceProvider = CreateProvider();
            DiscordSocketConfig config = new DiscordSocketConfig
            {
                GatewayIntents = Discord.GatewayIntents.AllUnprivileged
            };
            this.client = new DiscordSocketClient(config);
            this.random = new Random();
            AllResults.blacklist = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt");
        }

        static IServiceProvider CreateProvider()
        {
            var collection = new ServiceCollection();
            //...
            return collection.BuildServiceProvider();
        }

        public async Task StartBotAsync()
        {
            client.Log += message =>
            {
                Console.WriteLine(message);
                return Task.CompletedTask;
            };
            client.Ready += ClientReady;
            await this.client.StartAsync();
            string token = System.IO.File.ReadAllText("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\token.txt");
            await this.client.LoginAsync(Discord.TokenType.Bot, token);
            CreateAllResults();
            CreateNewResults();
            CreateStonehearts();
            Console.WriteLine("There are currently " + AllResults.allResults.Count + " different things you can pull.");
            Console.WriteLine($"There are {AllResults.RemainingSupremeGamblers()} Supreme Gamblers left.");
            await Task.Delay(-1);
        }

        public async Task ClientReady()
        {
            Console.WriteLine("Registering commands");
            InteractionServiceConfig config = new InteractionServiceConfig()
            {
                AutoServiceScopes = true
            };
            this.interactionService = new InteractionService(client.Rest, config);
            try
            {
                await interactionService.AddModulesAsync(Assembly.GetExecutingAssembly(), _serviceProvider);
                Console.WriteLine("Added modules");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                await interactionService.RegisterCommandsGloballyAsync();
                Console.WriteLine("Registered commands");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            client.InteractionCreated += async interaction =>
            {
                var scope = _serviceProvider.CreateScope();
                var ctx = new SocketInteractionContext(client, interaction);
                await interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
            };
        }

        void CreateAllResults()
        {
            GambleResult result = new GambleResult();

            result.name = "Yanqing";
            result.imageURL = "https://upload-os-bbs.hoyolab.com/upload/2023/05/23/130859530/affcd9632469a07026f8ad61ef76bc8a_679955486519927135.png";
            result.reaction = "THE GOAT";
            result.color = Color.Gold;
            AllResults.allResults.Add(result);

            result.name = "Orange with Mayo";
            result.imageURL = "https://cdn.discordapp.com/emojis/1252274717998452776.webp";
            result.reaction = "Best food of them all.";
            result.color = Color.Orange;
            AllResults.allResults.Add(result);

            result.name = "Copestance";
            result.imageURL = "https://cdn.discordapp.com/emojis/1255933370567884853.webp";
            result.reaction = "She'll release in 2.5, no, 2.6, no wait, 2.7?.. 3.0, surely.";
            result.color = new Color(167, 139, 250);
            AllResults.allResults.Add(result);

            result.name = "Buddygame";
            result.imageURL = "https://media.discordapp.net/attachments/1206405231089557544/1220473802325102622/image.png?ex=66e8994d&is=66e747cd&hm=03dfff6dc7f6899b37ebfbbc09085601cc4ad2421d8131be4f4c29795c259d17&=&format=webp&quality=lossless";
            result.reaction = "It'll release in N days";
            result.color = Color.Green;
            AllResults.allResults.Add(result);

            result.name = "Stelle";
            result.imageURL = "https://cdn.discordapp.com/avatars/878460078959755264/c0df2729695d56a481ad8e909722306b.png?size=1024";
            result.reaction = "Silly racoon";
            result.color = new Color(255, 215, 0);
            AllResults. allResults.Add(result);

            result.name = "Shiroko";
            result.imageURL = "https://i.ytimg.com/vi/0rPqP8ZHv70/hq720.jpg";
            result.reaction = "Cutest cutie to ever cute";
            result.color = new Color(23, 247, 255);
            AllResults.allResults.Add(result);

            result.name = "Kayoko";
            result.imageURL = "https://i.ytimg.com/vi/E-FrUkx0nyo/hq720.jpg";
            result.reaction = "Cutest cutie to ever cute";
            result.color = new Color(59, 59, 59);
            AllResults.allResults.Add(result);

            result.name = "Booba sword";
            result.imageURL = "https://media.discordapp.net/attachments/1262986339217833984/1285028475199033375/image.png?ex=66e8c787&is=66e77607&hm=a01444a062df4093c44601ab7c7344bdff4f7082a51e943b7194e4aec3d25069&=&format=webp&quality=lossless";
            result.reaction = "Make sure not to cut yourself!";
            result.color = new Color(132, 0, 255);
            AllResults.allResults.Add(result);

            result.name = "Ei";
            result.imageURL = "https://cdn.discordapp.com/attachments/1286801789458579497/1286828023278207017/image.png?ex=66ef537c&is=66ee01fc&hm=5d5cae0c20fdb3acc65158f665b7b17f76e3ba538b8a9b47911abacffcdf56ac&";
            result.reaction = "Trusty bar owner";
            result.color = new Color(132, 0, 255);
            AllResults.allResults.Add(result);

            result.name = "Venti";
            result.imageURL = "https://www.gamerstopia.com/wp-content/uploads/2021/03/Venti-Genshin-Impact-Character-Profle-Featured-Image.jpg";
            result.reaction = "Our favorite femboy";
            result.color = new Color(0, 255, 64);
            AllResults.allResults.Add(result);

            result.name = "Screwllum";
            result.imageURL = "https://i1.sndcdn.com/artworks-tLYnBAQylWz5eckz-JXsDHg-t500x500.jpg";
            result.reaction = "Oh hey, that's me!";
            result.color = new Color(255, 247, 0);
            AllResults.allResults.Add(result);

            result.name = "Cyno";
            result.imageURL = "https://cdn.oneesports.gg/cdn-data/2022/09/GenshinImpact_Cyno_featured.jpg";
            result.reaction = "No, Cyno, March isn't here yet";
            result.color = new Color(255, 200, 71);
            AllResults.allResults.Add(result);

            result.name = "Jingliu";
            result.imageURL = "https://cdn.discordapp.com/emojis/1251252333153751142.webp";
            result.reaction = "Children spotted";
            result.color = new Color(105, 255, 250);
            AllResults.allResults.Add(result);

            result.name = "Yae Miko";
            result.imageURL = "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/02/Yae-Miko-in-Genshin-Impact.jpg";
            result.reaction = "The Seducer";
            result.color = new Color(255, 51, 228);
            AllResults.allResults.Add(result);

            result.name = "Jing Yuan";
            result.imageURL = "https://static1.thegamerimages.com/wordpress/wp-content/uploads/2023/05/honkai-star-rail-jing-yuan-thinking-with-bird-on-shoulder.jpg";
            result.reaction = "The Seducee";
            result.color = new Color(245, 255, 107);
            AllResults.allResults.Add(result);

            result.name = "Buffdollar";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285181838427689032/anchoragefronthands.jpg?ex=66e9565b&is=66e804db&hm=3e27e8058db18d2b384570c6ac0ca42266bb3d8f221f2c0ff279df53e5e4264a&";
            result.reaction = "Run, nerfcent... Run.";
            result.color = new Color(201, 201, 201);
            AllResults.allResults.Add(result);

            result.name = "Immortal's Delight";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/e/e8/Item_Immortal%27s_Delight.png";
            result.reaction = "Who doesn't love boba?";
            result.color = new Color(255, 215, 130);
            AllResults.allResults.Add(result);

            result.name = "Topass";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285183228944322623/topass.png?ex=66e957a7&is=66e80627&hm=ae3ca21a6b89d9a38c764533980a8615b6aff2ee414040e9f9caa77b1b2999b7&";
            result.reaction = "IPC's finest";
            result.color = new Color(255, 31, 72);
            AllResults.allResults.Add(result);

            result.name = "Dead Welt";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285183646692544544/deadweltreaction.png?ex=66e9580a&is=66e8068a&hm=174fee575af96aba0e06da61d51e9bd759baee80c922cceb98a914c588ae3352&";
            result.reaction = "It's so sad that Welt Yang died from ligma";
            result.color = new Color(181, 78, 0);
            AllResults.allResults.Add(result);

            result.name = "Girls kissing";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285184189993586740/IMG_6607.jpg?ex=66e9588c&is=66e8070c&hm=0095667ffbec0ea8eea3d2c60e0bdc726d57849a1a3efa46bdd8cfa63e813d43&";
            result.reaction = "GISSING";
            result.color = new Color(255, 0, 162);
            AllResults.allResults.Add(result);

            result.name = "Dancing Shiro";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285184625190244433/ezgif-5-617f3173ff.gif?ex=66e958f4&is=66e80774&hm=6df2c15ed7840f8c9a63f63cbce0bef4fc91b735b673f71e79970040f28bd786&";
            result.reaction = "She happy";
            result.color = new Color(23, 247, 255);
            AllResults.allResults.Add(result);

            result.name = "Sparkle Fumo";
            result.imageURL = "https://media1.tenor.com/m/SHUWQ809iMkAAAAd/niange-sparkle.gif";
            result.reaction = "Don't you even think about it...";
            result.color = Color.Red;
            AllResults.allResults.Add(result);

            result.name = "Scaramouche";
            result.imageURL = "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/08/Genshin-Impact-The-Lore-Behind-Harbinger-Scaramouche-Explained.jpg";
            result.reaction = "The wolf is coming";
            result.color = new Color(0, 4, 82);
            AllResults.allResults.Add(result);

            result.name = "Horse";
            result.imageURL = "https://cdn.britannica.com/92/1292-050-92C981ED/Appaloosa-mare-bay-colouring.jpg";
            result.reaction = "Horse";
            result.color = new Color(97, 42, 0);
            AllResults.allResults.Add(result);

            result.name = "Golden Hind";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285186818597457940/Golden_HindChibi.jpg?ex=66e95aff&is=66e8097f&hm=51813fa580ff43d53f6f21e57635c08574b07e9111143d6ce9405caa522173ab&";
            result.reaction = "*squish squish*";
            result.color = new Color(31, 31, 31);
            AllResults.allResults.Add(result);

            result.name = "Acheron";
            result.imageURL = "https://wallpapers.com/images/featured/plain-black-background-02fh7564l8qq4m6d.jpg";
            result.reaction = "...She got lost on the way here";
            result.color = new Color(0, 0, 0);
            AllResults.allResults.Add(result);

            result.name = "The Event Team";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285247319377313844/download_73.jpg?ex=66e99357&is=66e841d7&hm=5b26054de9971ddfc4fcb3b91f2cf87dfa621ff3326e102c68ad595a52fd87af&";
            result.reaction = "**THE EVENT TEAM IS COMPETENT**";
            result.color = Color.Red;
            AllResults.allResults.Add(result);

            result.name = "Pineapple on pizza";
            result.imageURL = "https://images.slurrp.com/prod/recipe_images/taste/bacon-cheddar-pineapple-pizza-1619714141_OQNFZCQBVPU4LJ0RNI3L.webp";
            result.reaction = "It's actually really good";
            result.color = new Color(255, 204, 0);
            AllResults.allResults.Add(result);

            result.name = "ORT eating you";
            result.imageURL = "https://static.wikia.nocookie.net/fategrandorder/images/7/7c/ORT_Portrait.png/revision/latest?cb=20230205125557";
            result.reaction = "ORT just ate you. That's it.";
            result.color = new Color(0, 38, 255);
            AllResults.allResults.Add(result);

            result.name = "Cyno's body pillow";
            result.imageURL = "https://cdn.discordapp.com/attachments/1171251045947682876/1285191737589108838/ezgif-4-fc8f064079.gif?ex=66e95f93&is=66e80e13&hm=0aede86613c446c63ec0fa89015d51483e9650e741c419359741a47ea13376e5&";
            result.reaction = "Might be a bit sticky";
            result.color = new Color(94, 212, 255);
            AllResults.allResults.Add(result);

            result.name = "Winged Hussars";
            result.imageURL = "https://d1lss44hh2trtw.cloudfront.net/resize?type=webp&url=https%3A%2F%2Fshacknews-ugc.s3.amazonaws.com%2Fuser%2F10200323%2Farticle%2F2022-04%2Ffeature_winged-hussars-cortex-post-shacknews.jpg%3FversionId%3DQEL2584C7BOtaxEq8MDG88oDM3hlJGMx&width=1032&sign=a0TnLqeBzTMqavsywQo6laZjPq3uPtBuAPk3ARsdrNg";
            result.reaction = "**POLSKA GUROM**";
            result.color = Color.Red;
            AllResults.allResults.Add(result);

            result.name = "beanie";
            result.imageURL = "https://static.wikia.nocookie.net/mrfz/images/f/fd/Exusiai_Skin_2.png/revision/latest?cb=20200201075206";
            result.reaction = "I made this bot btw";
            result.color = new Color(255, 51, 51);
            AllResults.allResults.Add(result);

            result.name = "Qiqi";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285197889756332054/755.png?ex=66e9654e&is=66e813ce&hm=46efedd1de8535a9aa4c1a9f6e3d1a051b01ff42bec699a91ba2a4bc3efb2014&";
            result.reaction = "Abundance cutie";
            result.color = new Color(79, 94, 196);
            AllResults.allResults.Add(result);

            result.name = "MORT";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285202993129590866/Picsart_24-08-18_19-17-36-564.jpg?ex=66e96a0f&is=66e8188f&hm=474f745e8ed7e4277d4436c12690103252a8790fc25473e7821df8f50d327771&";
            result.reaction = "*sniff*";
            result.color = new Color(0, 38, 255);
            AllResults.allResults.Add(result);

            result.name = "Spgylass Moment";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285221417280999444/Woe_Spyglass_moment_upon_ye.jpg?ex=66e97b38&is=66e829b8&hm=7e9487ed9c39789d4846dc2b9daa86b2840a94360b9681aeea133e3cb0ab1649&";
            result.reaction = "Can nevre have enouhg typos!";
            result.color = new Color(204, 204, 204);
            AllResults.allResults.Add(result);

            result.name = "Yumeko";
            result.imageURL = "https://cdn.tupperbox.app/pfp/904745046329397249/m9VFIEfxmx05k-_j.webp";
            result.reaction = "Time travelling cutie";
            result.color = new Color(255, 61, 61);
            AllResults.allResults.Add(result);

            result.name = "a spot in Trazyn's collection";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285232172730679316/image.png?ex=66e9853c&is=66e833bc&hm=53bda5daab40a0ddffa704a75c41aacfbfc701e4c0a29fe6f808f667e36a400c&";
            result.reaction = "Gotta catch 'em all";
            result.color = new Color(0, 153, 0);
            AllResults.allResults.Add(result);
                
            result.name = "Nothing";
            result.imageURL = null;
            result.reaction = "lol";
            result.color = new Color(0, 0, 0);
            AllResults.allResults.Add(result);

            result.name = "Pineapple";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285245330882957394/Screenshot_2024-09-14-08-28-57-64_f940ae8776cd4aedda1adc27c5d1f2d6.png?ex=66e9917d&is=66e83ffd&hm=f7a2ada9e655c7489dfbd92a8eb7c637bf78bb4baafd6b23832197a863ffd020&";
            result.reaction = "This doesn't look edible...";
            result.color = new Color(130, 26, 49);
            AllResults.allResults.Add(result);

            result.name = "Buff Feixiao";
            result.imageURL = "https://cdn.discordapp.com/attachments/1262986339217833984/1285251634066231347/GTAhAmSbMAAjjaQ.jpg?ex=66e9975c&is=66e845dc&hm=49ea5cd4fe34ff37108ea6bc6865c267f8652939def0d09071bf0aaf416e40ce&";
            result.reaction = "Take a break, have a fox woman";
            result.color = new Color(113, 222, 211);
            AllResults.allResults.Add(result);

            result.name = "Numbar";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285275917559529654/ballin.jpeg?ex=66e9adfa&is=66e85c7a&hm=463637cfe8b2afa88be8aa3714dcce6394706d7d49de8853bad0106ccc45d475&";
            result.reaction = "300 mg Benadryl no sleep";
            result.color = new Color(38, 44, 64);
            AllResults.allResults.Add(result);

            result.name = "Satoru Gojo";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285312981453766768/20240916_155443.jpg?ex=66e9d07e&is=66e87efe&hm=d2f7f405b7af9c045e3022ae9fa3ffa0c1484ddca7044d786d19e5b5fda65d21&";
            result.reaction = "Would you lose?";
            result.color = new Color(255, 255, 255);
            AllResults.allResults.Add(result);

            result.name = "Go/jo";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285314478149337108/image0.jpg?ex=66e9d1e3&is=66e88063&hm=56d69d244e6e98dba48864be9fa39a3693b10e66e568fc16c993bd4876cc368c&";
            result.reaction = "You were magnificent, Satoru Gojo. I won't forget you for as long as I live.";
            result.color = new Color(255, 255, 255);
            AllResults.allResults.Add(result);

            result.name = "JoGOAT";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285315199431086091/image0.jpg?ex=66e9d28f&is=66e8810f&hm=56907244f80456e0d3999c56d897494c49fa99545c6d3b5ad2edd616af0864ab&";
            result.reaction = "As the strongest Curse, Jogoat, fought the fraud, the King of Curses, he began to open his Domain. Sukuna shrunk back in fear, then Jogo said \"Stand proud, Sukuna. You're strong.\"";
            result.color = new Color(131, 177, 201);
            AllResults.allResults.Add(result);

            result.name = "Kagurabachi";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285316143778631680/image0.jpg?ex=66e9d370&is=66e881f0&hm=178c5879dd288337f0cce302a9f40996d25d60cf082a72888a16692715b5cca4&";
            result.reaction = "Enough time has passed.";
            result.color = new Color(0, 0, 0);
            AllResults.allResults.Add(result);

            result.name = "Sunday";
            result.imageURL = "https://media1.tenor.com/m/Hjdpl6IDoDsAAAAC/honkai-star-rail-honkai-star-rail-sunday.gif";
            result.reaction = "He's never dropping...";
            result.color = new Color(255, 255, 255);
            AllResults.allResults.Add(result);

            result.name = "Black Blade";
            result.imageURL = "https://cdn.discordapp.com/attachments/1285365379438477403/1285588050914578502/IMG_20231204_113412.png?ex=66ead0ac&is=66e97f2c&hm=f4bba39b1c2df9a35b0a0c7406554de6fde95f0b9feaa835747fc8d14f66f505&";
            result.reaction = "I'm gonna fucking say it";
            result.color = new Color(155, 126, 111);
            AllResults.allResults.Add(result);

            result.name = "Jonathan Hanabi Sparkle";
            result.imageURL = "https://i1.sndcdn.com/artworks-Ly6oMdoj6JeXe7Bz-20jjYg-t500x500.jpg";
            result.reaction = "I heard there was a horse somewhere around here?";
            result.color = new Color(179, 36, 36);
            AllResults.allResults.Add(result);

            result.name = "Fox of Judgement";
            result.imageURL = "https://i.pinimg.com/474x/40/71/4b/40714bff9ed7c3ebbe54963820ee6e0a.jpg";
            result.reaction = "*Judging you*";
            result.color = new Color(189, 178, 162);
            AllResults.allResults.Add(result);

            result.name = "Totally not a Eula alt";
            result.imageURL = "https://cdn.discordapp.com/attachments/1277656260333928554/1285632019027722360/eula_alt.png?ex=66eaf99f&is=66e9a81f&hm=aa67ccf2dc2314cd0e9766b8e35f4935ea70723dcf96c650dedacb1928f22565&";
            result.reaction = "Hi everyone! Anyway, I hate men-";
            result.color = new Color(138, 228, 242);
            AllResults.allResults.Add(result);

            result.name = "Kosaka Wakamer";
            result.imageURL = "https://media1.tenor.com/m/qel4V5Xum9YAAAAd/wakamow.gif";
            result.reaction = "I'm sorry I burneded them all";
            result.color = new Color(227, 25, 45);
            AllResults.allResults.Add(result);

            result.name = "Akira Kiyosumer";
            result.imageURL = "https://cdn.discordapp.com/attachments/1226595043297726525/1285634524235038833/IMG_7458.png?ex=66eafbf4&is=66e9aa74&hm=e917ce6b966e5bd895b825de2540c13ef52bc8968d06be03850f7b597365de48&";
            result.reaction = "I'm sorry I stealeded them all";
            result.color = new Color(255, 242, 253);
            AllResults.allResults.Add(result);

            result.name = "The Black Swanling";
            result.imageURL = "https://cdn.discordapp.com/attachments/1278576094315810917/1278576605018587146/Untitled533_20240213210315.png?ex=66eb0401&is=66e9b281&hm=6c2254f422988069e25b8ddf6834e6e49292ae513a0f34d88d5e26e978c74add&";
            result.reaction = "Congratulations! You just pulled the GREATEST and MOST AWESOMEST MAGICIAN IN THE COSMOS!!";
            result.color = new Color(95, 15, 186);
            AllResults.allResults.Add(result);

            result.name = "Huohuo";
            result.imageURL = "https://safebooru.org//samples/4619/sample_5cf0742e218e6d03c65b253a1bcf6da752213c04.jpg";
            result.reaction = "Ten-Lords commission cutie";
            result.color = new Color(142, 209, 152);
            AllResults.allResults.Add(result);

            result.name = "Tail";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/e/ef/NPC_Tail_Unleashed.png";
            result.reaction = "*<This message has been removed by the Ten-Lords commission>*";
            result.color = new Color(142, 209, 152);
            AllResults.allResults.Add(result);

            result.name = "Numby T. Trotter trust fund";
            result.imageURL = "https://cdn.discordapp.com/attachments/1277656260333928554/1285660405888323594/Numbar.webp?ex=66eb140f&is=66e9c28f&hm=3480aba561e9ca2e66510fafa515c91eefcf7bd1a8b67e76756b21c2331f5dd4&";
            result.reaction = "Never Stop Gambling";
            result.color = new Color(38, 44, 64);
            AllResults.allResults.Add(result);

            result.name = "Lil Gui";
            result.imageURL = "https://cdn.discordapp.com/attachments/1192874849144684657/1285731031168843890/4492c12af6c9a04592e4834e550d10e7_4018867837588071565.png?ex=66eb55d5&is=66ea0455&hm=210e069d19fcf5f0bb70f0f03457623ee641d859cd2e81e5ba4bf1068cda9a23&";
            result.reaction = "Resident yingxing fucker";
            result.color = new Color(255, 132, 94);
            AllResults.allResults.Add(result);

            result.name = "Sandrone";
            result.imageURL = "https://pbs.twimg.com/media/F6du9qZXIAAsRB2?format=jpg&name=large";
            result.reaction = "No strings attached";
            result.color = new Color(227, 224, 197);
            AllResults.allResults.Add(result);

            result.name = "skrunklies";
            result.imageURL = "https://cdn.discordapp.com/attachments/1173422620964831334/1286014170135003250/ezgif-7-2685451b5e.gif?ex=66ec5d87&is=66eb0c07&hm=60b05c998469e14e75b90ef0c0c09f8e237112ed1cf4b1e22c5906966bd859bd&";
            result.reaction = "skrunkly";
            result.color = new Color(255, 255, 255);
            AllResults.allResults.Add(result);

            result.name = "Eepy Faux";
            result.imageURL = "https://i.redd.it/r5eqwetedr881.png";
            result.reaction = "Eep arp... eeerp...";
            result.color = new Color(255, 212, 218);
            AllResults.allResults.Add(result);

            result.name = "Frogs";
            result.imageURL = "https://cdn.discordapp.com/attachments/1285365379438477403/1286229710463832155/Frogs.gif?ex=66ed2643&is=66ebd4c3&hm=fee2beccfeb2c27f9c757ffb0f46e70126efeed8baf3ca480948736592d29c4c&";
            result.reaction = "It is Wednesday, my dudes";
            result.color = new Color(171, 135, 77);
            AllResults.allResults.Add(result);

            result.name = "Sethos";
            result.imageURL = "https://cdn.discordapp.com/attachments/1283852708654354543/1286803213055688704/1905CED7-B626-40E4-8595-4E06B9FB2A82.jpg?ex=66ef3c61&is=66edeae1&hm=7406916c2a4b50e7acc2a211587373d9b104c60c56f78726fdc580a42a1761f7&";
            result.reaction = "Sunday chaser";
            result.color = new Color(194, 105, 16);
            AllResults.allResults.Add(result);
        }

        void CreateNewResults()
        {
            GambleResult result = new GambleResult();

            result.name = "Pipebomb";
            result.imageURL = "https://cdn.discordapp.com/attachments/1251236009136357416/1287755711400644628/poll-what-is-your-favourite-healer-for-furina-and-what-v0-vsglfbxg7vuc1.jpg?ex=66f2b376&is=66f161f6&hm=e7cadb978881bc8c3aa96b81e882a33cc3f5129cc604a462c9c1d6415a8602a3&";
            result.reaction = "WOE";
            result.color = new Color(63, 62, 140);
            AllResults.newResults.Add(result);

            result.name = "Wincest";
            result.imageURL = "https://s1.zerochan.net/Honkai.Star.Rail.600.4088562.jpg";
            result.reaction = "Keeping it in the family";
            result.color = new Color(222, 222, 255);
            AllResults.newResults.Add(result);

            result.name = "Shiroko Terror";
            result.imageURL = "https://cdn.donmai.us/original/1e/df/__shiroko_and_shiroko_terror_blue_archive_drawn_by_m_r_b__1edf0a0b471bf9dccb0a18bd702ef49d.jpg";
            result.reaction = "Traumatized cutie";
            result.color = new Color(209, 209, 227);
            AllResults.newResults.Add(result);

            result.name = "ForRealium";
            result.imageURL = "https://cdn.discordapp.com/attachments/1251236009136357416/1287759695192264804/RDT_20240920_2151294061859324892751103.jpg?ex=66f2b72c&is=66f165ac&hm=9e96b5d6e4a521affa4486146e02e84d898b9bf498e791aae9f334736ccdaf28&";
            result.reaction = "ong?";
            result.color = new Color(255, 255, 255);
            AllResults.newResults.Add(result);

            result.name = "Headpatting Shiroko";
            result.imageURL = "https://cdn.donmai.us/original/0e/b9/__shiroko_blue_archive_drawn_by_haraheriei__0eb9a6d25ada00b190a871a5ba1e0932.png";
            result.reaction = "You just headpatted the Shirocutie!";
            result.color = new Color(23, 247, 255);
            AllResults.newResults.Add(result);

            result.name = "Dr. Ratioed";
            result.imageURL = "https://i.redd.it/2usrhhoo6l6c1.jpeg";
            result.reaction = "Genshin could never";
            result.color = new Color(95, 95, 237);
            AllResults.newResults.Add(result);

            result.name = "Live Ajaw Reaction";
            result.imageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQhaxZrzHorKgAB7bbei0C88eNPZduFTHQRLEB2j54EXLAFjLpixqag1vsP&s=10";
            result.reaction = "\"A-jaw dropping pull\" - Cyno";
            result.color = new Color(219, 255, 38);
            AllResults.newResults.Add(result);

            result.name = "Hunter Gaul";
            result.imageURL = "https://cdn.discordapp.com/attachments/1232034635169402971/1287764444025458739/83c2d6f20f8bc9457a9e6ff70f6ba2fd.png?ex=66f2bb98&is=66f16a18&hm=932098940179f10d620b82fb38bfbe967208f52887b2f5694cfc428480076c33&";
            result.reaction = "In my defense the S in \"Slay the monster\" was missing";
            result.color = new Color(176, 28, 5);
            AllResults.newResults.Add(result);

            result.name = "Ei's Cooking";
            result.imageURL = "https://cdn.discordapp.com/attachments/1286801789458579497/1287860195787018372/20231207_112119-2.jpg?ex=66f314c5&is=66f1c345&hm=5c2d60ef2675ea642054167a1e9211b13496c1c36b8ad8fae649b1c80f1f925e&";
            result.reaction = "Canon Ei moment";
            result.color = new Color(255, 247, 219);
            AllResults.newResults.Add(result);

            result.name = "Gissing";
            result.imageURL = "https://cdn.discordapp.com/attachments/1251236009136357416/1287757529874563194/image-32.png?ex=66f406a8&is=66f2b528&hm=375fdc6af1d708f45c4703c1256911f6696424c7a84d686a35d12ada9529c8b8&";
            result.reaction = "Jump in here!";
            result.color = new Color(67, 142, 247);
            AllResults.newResults.Add(result);

            result.name = "Venting Machine";
            result.imageURL = "https://preview.redd.it/venting-machine-v0-19qmmfmhv6nd1.png?width=1080&crop=smart&auto=webp&s=40f9f8652f447fb7151ec4a2d8dbf8e539254ce0";
            result.reaction = "What should I buy...";
            result.color = new Color(60, 163, 114);
            AllResults.newResults.Add(result);

            result.name = "Foxian Beauties Near You";
            result.imageURL = "https://cdn.discordapp.com/attachments/1283852708654354543/1288207327308152863/840EAC0D-47DD-4E83-BC78-EEB3E5ECA119.jpg?ex=66f45810&is=66f30690&hm=85de2a0e4a4a8c83587e49079325b78a00ef7cba33aded795c56a1d09fd1b839&";
            result.reaction = "CLICK NOW CLICK NOW CLICK NOW";
            result.color = new Color(48, 219, 168);
            AllResults.newResults.Add(result);

            result.name = "Hoshino";
            result.imageURL = "https://cdn.discordapp.com/attachments/1251236009136357416/1287765010919194664/Screenshot_20240923-211808.jpg?ex=66f40da0&is=66f2bc20&hm=7431e5244be5b0e18eb7cd9877719829b620313ce93d769046c26d82a3e707d5&";
            result.reaction = "*honk shoo honk shoo*";
            result.color = new Color(255, 171, 231);
            AllResults.newResults.Add(result);

            result.name = "Ben";
            result.imageURL = "https://cdn.discordapp.com/attachments/1283852708654354543/1288207328205737994/IMG_1363.jpg?ex=66f45810&is=66f30690&hm=6bdfb62fa85d17d9628620d52ef420293eb650c2010f3ed36fc87ce111bd80f3&";
            result.reaction = "You know what you did...";
            result.color = new Color(84, 45, 0);
            AllResults.newResults.Add(result);

            result.name = "Saturday";
            result.imageURL = "https://media1.tenor.com/m/u-7y_HU0mXkAAAAC/sunday-hsr.gif";
            result.reaction = "He IS dropping soon";
            result.color = new Color(255, 255, 255);
            AllResults.newResults.Add(result);

            result.name = "The Battle Maid Army";
            result.imageURL = "https://preview.redd.it/time-for-some-changes-in-the-ten-lords-commission-v0-vx8afpkyi3fc1.png?width=1330&format=png&auto=webp&s=21eb3d128ea0773c0478b164eb55bc320f86fbcf";
            result.reaction = "Luofu peakest army";
            result.color = new Color(247, 57, 57);
            AllResults.newResults.Add(result);

            result.name = "Nerfcent";
            result.imageURL = "https://safebooru.org//images/4619/5290522977f1aacb3c28f16b3ff199b04f120cb6.jpg?5150161";
            result.reaction = "The Secret Dev... ~~But not of this bot~~";
            result.color = new Color(255, 255, 255);
            AllResults.newResults.Add(result);

            result.name = "Yae Miko in Hu Tao cosplay";
            result.imageURL = "https://cdn.donmai.us/original/c1/e5/__hu_tao_and_boo_tao_genshin_impact_drawn_by_shuvalun8__c1e5a9ea457e78a9d4c09d092b7dec1d.jpg";
            result.reaction = "Get in the box.";
            result.color = new Color(59, 11, 0);
            AllResults.newResults.Add(result);

            result.name = "Venti in Xingqiu cosplay";
            result.imageURL = "https://i.redd.it/aa103lw5vrj71.jpg";
            result.reaction = "Blue haired twink cosplays blue haired twink";
            result.color = new Color(85, 154, 207);
            AllResults.newResults.Add(result);

            result.name = "The Everflame Mansion";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/7/73/HoYoLAB_Article_Header_Annihilation_Gang.png/revision/latest/scale-to-width-down/1000?cb=20240119050932";
            result.reaction = "Can't wait to pull for them in 7.2!";
            result.color = new Color(105, 0, 0);
            AllResults.newResults.Add(result);

            result.name = "<:balls:1288257347755573369>";
            result.imageURL = "https://cdn.7tv.app/emote/624c6b32e236ca9775ecbaa1/4x.webp";
            result.reaction = "<:balls:1288257347755573369>";
            result.color = new Color(0, 5, 107);
            AllResults.newResults.Add(result);

            result.name = "ORT";
            result.imageURL = "https://cdn.discordapp.com/attachments/1243882875913502720/1285312455844827186/images_15.jpg?ex=66f45c01&is=66f30a81&hm=ddebcc41d196a6a2c844e7ce31a786da1678e9f500b2d805604d4d500b9bd2fe&";
            result.reaction = "Congratulations, you pulled the bad ending.";
            result.color = new Color(0, 255, 251);
            AllResults.newResults.Add(result);

            result.name = "Gege Akutami";
            result.imageURL = "https://static.wikia.nocookie.net/jujutsu-kaisen/images/f/fd/Gege_Akutami.png/revision/latest/scale-to-width-down/200?cb=20181003050746";
            result.reaction = "Gege when I catch you Gege";
            result.color = new Color(255, 255, 255);
            AllResults.newResults.Add(result);

            result.name = "Literally Everything";
            result.imageURL = null;
            result.reaction = "I can't even give you a picture of it";
            result.color = new Color(255, 255, 255);
            AllResults.newResults.Add(result);

            result.name = "Kayoko shooting you";
            result.imageURL = "https://cdn.donmai.us/sample/85/f7/__kayoko_and_kayoko_blue_archive_drawn_by_fujifuji0330__sample-85f7e33378bc6441c771a45f70d2d025.jpg";
            result.reaction = "You've been hit by, you've been struck by, a cutie.";
            result.color = new Color(255, 252, 232);
            AllResults.newResults.Add(result);

            result.name = "Chibi Huohuo";
            result.imageURL = "https://i.redd.it/qepejfnkc08c1.png";
            result.reaction = "Green creature";
            result.color = new Color(142, 209, 152);
            AllResults.newResults.Add(result);

            result.name = "Porn";
            result.imageURL = "https://media1.tenor.com/m/LSXff3TbiWwAAAAd/you-are-not-normal-meme.gif";
            result.reaction = "This is no normal pull result...";
            result.color = new Color(0, 0, 0);
            AllResults.newResults.Add(result);

            result.name = "Unusual Hilichurl";
            result.imageURL = "https://static.wikia.nocookie.net/gensin-impact/images/a/ae/Enemy_Unusual_Hilichurl.png/revision/latest/scale-to-width-down/1000?cb=20210507082406";
            result.reaction = "...Hear me out";
            result.color = new Color(255, 242, 222);
            AllResults.newResults.Add(result);

            result.name = "Rappa";
            result.imageURL = "https://i.redd.it/zrzraf7oc7ld1.jpeg";
            result.reaction = "Totally not Mori Calliope";
            result.color = new Color(255, 31, 83);
            AllResults.newResults.Add(result);

            result.name = "Embryo of Philosophy, Sunday";
            result.imageURL = "https://static1.thegamerimages.com/wordpress/wp-content/uploads/2024/05/honkai-star-rail-sunday-boss-fight-start-scene.jpg";
            result.reaction = "The Stonehearts stand no chance";
            result.color = new Color(255, 182, 25);
            AllResults.newResults.Add(result);

            result.name = "Shampoo";
            result.imageURL = "https://cdn.discordapp.com/attachments/1199117309508329603/1289056680340095078/74_sin_titulo_20240608135450.png?ex=66f76f16&is=66f61d96&hm=4b7442af7c32ed7dfeeaa2f32a7b1aee0a6317a38aadf4e7e74f99881f5a4e8f&";
            result.reaction = "Your favorite shady salesman";
            result.color = new Color(56, 59, 156);
            AllResults.newResults.Add(result);

            result.name = "Moonlit Dancer Luna";
            result.imageURL = "https://img10.reactor.cc/pics/post/full/neon-%28pixiv-31150749%29-Anime-Artist-artist-Theresa-Apocalypse-8171119.png";
            result.reaction = "Rawr";
            result.color = new Color(184, 0, 21);
            AllResults.newResults.Add(result);

            result.name = "Brazilian Miku";
            result.imageURL = "https://img10.joyreactor.cc/pics/post/full/Doodly-Hatsune-Miku-Vocaloid-8564400.jpeg";
            result.reaction = "Ms. Worldwide";
            result.color = new Color(71, 255, 221);
            AllResults.newResults.Add(result);

            result.name = "Ceroba Ketsukane";
            result.imageURL = "https://preview.redd.it/roba-posting-v0-wdk46wq6jvtc1.jpg?width=736&format=pjpg&auto=webp&s=e2133dd9fd2160bfaa928a952d985094862a2e28";
            result.reaction = "you're a furry lmao";
            result.color = new Color(250, 172, 62);
            AllResults.newResults.Add(result);

            result.name = "Cheesecake";
            result.imageURL = "https://www.allrecipes.com/thmb/Pld3RNxDK3y7c0ieRqxaiupjvEs=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/8419-easy-sour-cream-cheesecake-DDMFS-beauty-4x3-BG-2747-44b427d330aa41aa876269b1249698a0.jpg";
            result.reaction = "\"The Best Dessert Ever\" - Luna";
            result.color = new Color(255, 228, 158);
            AllResults.newResults.Add(result);
        }

        void CreateStonehearts()
        {
            GambleResult result = new GambleResult();

            result.name = "Jade <:Jade_Cornerstone:1287894285240504360>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/6/6d/Character_Jade_Splash_Art.png/revision/latest/scale-to-width-down/1000?cb=20240706170539";
            result.color = new Color(94, 0, 217);
            AllResults.stonehearts.Add(result);

            result.name = "Obsidian <:Obsidian_Cornerstone:1287894270585475074>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/1/19/NPC_Obsidian.png/revision/latest?cb=20240714050757";
            result.color = new Color(163, 15, 15);
            AllResults.stonehearts.Add(result);

            result.name = "Opal <:Opal_Cornerstone:1287894245738418256>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/8/82/NPC_Opal.png/revision/latest?cb=20240714052605";
            result.color = new Color(255, 202, 110);
            AllResults.stonehearts.Add(result);

            result.name = "Pearl <:Pearl_Cornerstone:1287894257419550791>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/7/7e/NPC_Pearl.png/revision/latest?cb=20240714065429";
            result.color = new Color(224, 252, 255);
            AllResults.stonehearts.Add(result);

            result.name = "Topaz <:Topaz_Cornerstone:1287894297844383744>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/9/9d/Character_Topaz_and_Numby_Splash_Art.png/revision/latest/scale-to-width-down/1000?cb=20231030040101";
            result.color = new Color(255, 33, 44);
            AllResults.stonehearts.Add(result);

            result.name = "Agate <:Agate_Cornerstone:1287894344052899900>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/d/d0/NPC_Agate.png/revision/latest?cb=20240714051110";
            result.color = new Color(196, 43, 0);
            AllResults.stonehearts.Add(result);

            result.name = "Amber <:Amber_Cornerstone:1287894316915888220>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/9/90/NPC_Amber.png/revision/latest?cb=20240714050206";
            result.color = new Color(255, 167, 66);
            AllResults.stonehearts.Add(result);

            result.name = "Sapphire <:Sapphire_Cornerstone:1287894232908038165>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/0/02/NPC_Sapphire.png/revision/latest?cb=20240714043712";
            result.color = new Color(76, 117, 194);
            AllResults.stonehearts.Add(result);

            result.name = "Sugilite <:Sugilite_Cornerstone:1287894216621559853>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/f/f5/NPC_Sugilite.png/revision/latest?cb=20240714044802";
            result.color = new Color(135, 52, 199);
            AllResults.stonehearts.Add(result);

            result.name = "Aventurine <:Aventurine_Cornerstone:1287894329632755754>";
            result.imageURL = "https://static.wikia.nocookie.net/houkai-star-rail/images/a/a9/Character_Aventurine_Splash_Art.png/revision/latest/scale-to-width-down/1000?cb=20240327104723";
            result.color = new Color(40, 224, 181);
            AllResults.stonehearts.Add(result);
        }

        static async Task Main(string[] args)
        {
            var myBot = new Program();
            await myBot.StartBotAsync();
        }
    }

    public struct GambleResult
    {
        public string name;
        public string imageURL;
        public string reaction;
        public Color color;
    }

    public static class AllResults
    {
        public static Dictionary<SocketGuildUser, DateTimeOffset> cooldowns = new Dictionary<SocketGuildUser, DateTimeOffset>();
        public static List<GambleResult> allResults = new List<GambleResult>();
        public static List<GambleResult> newResults = new List<GambleResult>();
        public static List<GambleResult> stonehearts = new List<GambleResult>();
        public static string[] blacklist;

        public static string[] kissGifs = new string[]
        {
            "https://media1.tenor.com/m/rnO3NgoffHYAAAAC/bronya-seele.gif",
            "https://media1.tenor.com/m/OByUsNZJyWcAAAAC/emre-ada.gif",
            "https://media1.tenor.com/m/BZyWzw2d5tAAAAAC/hyakkano-100-girlfriends.gif",
            "https://media1.tenor.com/m/1SBrq4NinsEAAAAC/yoshikazu-kisses-kiyone-on-her-cheeks.gif",
            "https://media1.tenor.com/m/ye6xtORyw_8AAAAC/2.gif",
            "https://media1.tenor.com/m/NZUQilMD3IIAAAAd/horimiya-izumi-miyamura.gif",
            "https://media1.tenor.com/m/cQzRWAWrN6kAAAAC/ichigo-hiro.gif",
            "https://media1.tenor.com/m/Daj-Pn82PagAAAAC/gif-kiss.gif",
            "https://media1.tenor.com/m/xoagmdkHtukAAAAC/kiss-anime-lol.gif",
            "https://media1.tenor.com/m/vtOmnXkckscAAAAC/kiss.gif",
            "https://media.tenor.com/f2hkRNjOGmkAAAAi/jun-lemon-loved.gif",
            "https://media1.tenor.com/m/MtKkjQF4rz4AAAAC/kiss-anime.gif",
            "https://media1.tenor.com/m/2tB89ikESPEAAAAC/kiss-kisses.gif",
            "https://media1.tenor.com/m/g9HjxRZM2C8AAAAd/anime-love.gif",
            "https://media1.tenor.com/m/Fyq9izHlreQAAAAC/my-little-monster-haru-yoshida.gif"
        };

        public static string[] hugGifs = new string[]
        {
            "https://media1.tenor.com/m/Xm0wrM7RXkAAAAAC/mihoyo-genshin-impact.gif",
            "https://media1.tenor.com/m/eQr2BfPq2OAAAAAC/genshin-impact-ember.gif",
            "https://media1.tenor.com/m/1_0ZOurJMSsAAAAd/genshin-impact-genshin.gif",
            "https://media1.tenor.com/m/NcryFMp4EpAAAAAd/bronyaxseele.gif",
            "https://media1.tenor.com/m/2HxamDEy7XAAAAAC/yukon-child-form-embracing-ulquiorra.gif",
            "https://media1.tenor.com/m/kCZjTqCKiggAAAAC/hug.gif",
            "https://media1.tenor.com/m/J7eGDvGeP9IAAAAC/enage-kiss-anime-hug.gif",
            "https://media1.tenor.com/m/UnpQCW40JekAAAAC/anime-hug.gif",
            "https://media1.tenor.com/m/NgriQrF0JbkAAAAC/anime-cuddle.gif",
            "https://media1.tenor.com/m/Qe6rYPM-FMkAAAAC/prsk-pjsekai.gif",
            "https://media1.tenor.com/m/UUDWXyIeKvkAAAAC/hug.gif",
            "https://media1.tenor.com/m/tpXMRHSykOAAAAAC/anime.gif",
            "https://media1.tenor.com/m/8Jk1ueYnyYUAAAAC/hug.gif",
            "https://media1.tenor.com/m/QwHSis0hNEQAAAAC/love-hug.gif"
        };

        public static string[] punchGifs = new string[]
        {
            "https://media1.tenor.com/m/JugpEiuzcOcAAAAd/yae-miko-genshin-impact.gif",
            "https://media1.tenor.com/m/z3mj0TPNF_sAAAAd/gi-meme.gif",
            "https://media1.tenor.com/m/TbcRpwUmpiAAAAAC/yae-miko-punch.gif",
            "https://media1.tenor.com/m/PEmakDKCWIwAAAAC/heizou-shikanoin.gif",
            "https://media1.tenor.com/m/d60sUzi8MioAAAAC/luka-luka-honkai.gif",
            "https://media1.tenor.com/m/6upHFF_DT-QAAAAd/svarog-punch.gif",
            "https://media1.tenor.com/m/pZ9vZr4QwtMAAAAC/devastation-punch.gif",
            "https://media1.tenor.com/m/y7UtaIlqz3YAAAAd/rudy-rudilous-firefly.gif",
            "https://media1.tenor.com/m/BoYBoopIkBcAAAAC/anime-smash.gif",
            "https://media1.tenor.com/m/qDDsivB4UEkAAAAC/anime-fight.gif",
            "https://media1.tenor.com/m/YGKPpkNN6g0AAAAC/anime-jujutsu-kaisen-anime-punch.gif",
            "https://media1.tenor.com/m/Kbit6lroRFUAAAAC/one-punch-man-saitama.gif",
            "https://media1.tenor.com/m/gmvdv-e1EhcAAAAC/weliton-amogos.gif",
            "https://media1.tenor.com/m/pWXJ5NlI-g0AAAAC/one-punch-man-anime.gif",
            "https://media1.tenor.com/m/nWTDZU5WQ4oAAAAC/anime-punching.gif",
            "https://media1.tenor.com/m/8uNoClyIg6cAAAAC/senator-armstrong-metal-gear-rising.gif"
        };

        public static string[] patGifs = new string[]
        {
            "https://media1.tenor.com/m/EP6GJ7kwm5wAAAAd/razor-genshin-impact-genshin-impact.gif",
            "https://media1.tenor.com/m/wVYbN3I_uRMAAAAd/yae-miko-yae.gif",
            "https://media1.tenor.com/m/3FyFVjLmHAsAAAAd/noelle-genshin.gif",
            "https://media1.tenor.com/m/l2wve2Ik724AAAAd/nahida-genshin-impact.gif",
            "https://media1.tenor.com/m/KyGPQuYCdYkAAAAd/pat-garrys-mod.gif",
            "https://media1.tenor.com/m/1I1pGUd3xWkAAAAC/mala-mishra-jha-pat-head.gif",
            "https://media1.tenor.com/m/s7lGkoIAieYAAAAC/so-cute-cat.gif",
            "https://media1.tenor.com/m/ynFUoAowiP4AAAAC/patpat-pat.gif",
            "https://media1.tenor.com/m/V1Txxwwe0d8AAAAC/anime-head-pat-anime.gif",
            "https://media1.tenor.com/m/GC9rg-v-wvMAAAAC/anime-pat.gif",
            "https://media1.tenor.com/m/bPPe8Kipg0gAAAAd/shiroko-headpat.gif",
            "https://media1.tenor.com/m/H8srViwnvmcAAAAd/izuna-blue-archive.gif",
            "https://media1.tenor.com/m/vX-FlhfSVk4AAAAC/honkai-honkai-star-rail.gif",
            "https://media1.tenor.com/m/_RiBHVVH-wIAAAAC/kafka-kafka-pat.gif",
            "https://media1.tenor.com/m/s0zK0WJlXpAAAAAd/firefly-%E6%B5%81%E8%90%A4.gif"
        };

        public static string[] throwGifs = new string[]
        {
            "https://media1.tenor.com/m/Ia58SdP_5MIAAAAC/lion-king-toss.gif",
            "https://media1.tenor.com/m/LpwpMlWG04EAAAAC/corey-tonge-thrown-out-of-the-window.gif",
            "https://media1.tenor.com/m/qCTnY5ry2gEAAAAd/anime-throw.gif",
            "https://media1.tenor.com/m/BUZOVzZoN-gAAAAC/get-up-out-of-my-bed-anime.gif",
            "https://media1.tenor.com/m/pRW5ZWeLeCEAAAAC/suplex-throw.gif",
            "https://media1.tenor.com/m/fMvO7xDLw1IAAAAd/out-the-window-toss.gif"
        };

        public static string[] stabGifs = new string[]
        {
            "https://media1.tenor.com/m/4g8e9j34i2oAAAAd/ling-han-stab.gif",
            "https://media1.tenor.com/m/Ule9Bq4pw8wAAAAd/killer-ayaka.gif",
            "https://media1.tenor.com/m/G-C8lVd-G8EAAAAd/firefly-stabbed.gif",
            "https://media1.tenor.com/m/wrj0rRzXkoMAAAAC/please-beg.gif",
            "https://media1.tenor.com/m/gk--YeV-lJkAAAAC/stabby-stab-koishi.gif",
            "https://media1.tenor.com/m/CWsbJDl70tsAAAAC/shimoneta.gif"
        };

        public static string[] slapGifs = new string[]
        {
            "https://media1.tenor.com/m/eU5H6GbVjrcAAAAC/slap-jjk.gif",
            "https://media1.tenor.com/m/XiYuU9h44-AAAAAC/anime-slap-mad.gif",
            "https://media1.tenor.com/m/8bSm0lI4_FUAAAAC/yuuri.gif",
            "https://media1.tenor.com/m/E3OW-MYYum0AAAAC/no-angry.gif",
            "https://media1.tenor.com/m/q8v19fyTzmMAAAAd/firefly-sam.gif",
            "https://media1.tenor.com/m/Db72dfVmRUoAAAAC/anime-game.gif",
            "https://media1.tenor.com/m/IROtP5rDhRsAAAAd/venti-venti-slap.gif"
        };

        public static string[] biteGifs = new string[]
        {
            "https://media1.tenor.com/m/qJFIZnigq9oAAAAC/holo-wise-wolf.gif",
            "https://media.tenor.com/HuE9vf7guRoAAAAi/lily-and-marigold.gif",
            "https://media1.tenor.com/m/Yk5VLCkqfwYAAAAC/chomp-bite-arm.gif",
            "https://media1.tenor.com/m/5mVQ3ffWUTgAAAAC/anime-bite.gif",
            "https://media1.tenor.com/m/IKDf1NMrzsIAAAAC/anime-acchi-kocchi.gif",
            "https://media1.tenor.com/m/jQ1anSa1FekAAAAC/bite-me.gif",
            "https://media1.tenor.com/m/u9x1__UkHZcAAAAC/mashiroiro-symphony-pure-white-symphony.gif",
            "https://media1.tenor.com/m/L0dUwfWV8owAAAAC/hu-tao.gif",
            "https://media1.tenor.com/m/cSvwPoClCaQAAAAC/nom-nom.gif",
            "https://media.tenor.com/VXqink0UhmcAAAAi/bite.gif"
        };

        public readonly static string[] relationships = new string[]
        {
            "USER_2 is USER_1's parent",
            "USER_1 and USER_2 are siblings",
            "USER_1 and USER_2 are just friends",
            "USER_1 and USER_2 are strangers",
            "USER_1 and USER_2 are different species",
            "USER_1 and USER_2 are ex lovers",
            "USER_1 and USER_2 are married",
            "USER_1 and USER_2 are dating",
            "USER_1 and USER_2 are fighting to death",
            "USER_2 is USER_1's grandparent",
            "USER_1 and USER_2 are playing UNO together",
            "USER_1 and USER_2 are yapping buddies",
            "USER_1 and USER_2 are on a Crusade together",
            "USER_1 and USER_2 are reading peak fiction together",
            "USER_1 and USER_2 are frauds",
            "USER_1 and USER_2 are mahjong buddies",
            "USER_2 murdered USER_1"
        };

        public readonly static string[] nsfwRelationships = new string[]
        {
            "USER_1 and USER_2 are having sex",
            "USER_1 and USER_2 are siblings, having sex",
            "USER_1 and USER_2 had a one-night stand and that's it",
            "USER_1 and USER_2 are rizzing each other's ohio",
            "USER_1 and USER_2 are friends with benefits",
            "USER_1 and USER_2 are gooning 24/7",
        };

        public static int punchCounter = 0;

        const ulong supremeGamblerRoleId = 1285960546365214832;

        public static async Task ReassignSupremeGambler(IUser newGambler, IGuild guild)
        {
            try
            {
                string userId = System.IO.File.ReadLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt").First();
                IGuildUser user = null;
                if (userId != string.Empty && userId != null)
                {
                    user = guild.GetUsersAsync().Result.First(x => x.Id.ToString() == userId);
                }
                if (user != null)
                {
                    if (RemainingStonehearts() > 0)
                        await user.RemoveRoleAsync(supremeGamblerRoleId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                if (newGambler != null)
                {
                    if (!System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt").Contains(newGambler.Id.ToString()))
                    {
                        if (RemainingStonehearts() > 0)
                        {
                            using (StreamWriter file = new StreamWriter("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt"))
                            {
                                file.Write(newGambler.Id.ToString());
                                file.Close();
                            }
                        }
                        else
                        {
                            using (StreamWriter file = new StreamWriter("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt", true))
                            {
                                file.WriteLine(newGambler.Id.ToString());
                                file.Close();
                            }
                        }
                        await (newGambler as SocketGuildUser).AddRoleAsync(supremeGamblerRoleId);
                    }
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static int RemainingSupremeGamblers()
        {
            return System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt").Where(x => x != string.Empty).Count();
        }

        public static async Task<IGuildUser> RemoveRandomSupremeGambler(IUser caller, IGuild guild)
        {
            string[] supremeGamblers = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt");
            if (!supremeGamblers.Contains(caller.Id.ToString()))
            {
                return null;
            }
            else if (RemainingSupremeGamblers() > 1 && !supremeGamblers.Where(x => x != string.Empty).All(x => x == caller.Id.ToString()))
            {
                Random random = new Random();
                int randomSG = random.Next(supremeGamblers.Length);
                if (supremeGamblers[randomSG] == caller.Id.ToString() || supremeGamblers[randomSG] == string.Empty)
                {
                    randomSG = random.Next(supremeGamblers.Length);
                }
                IGuildUser user = (await guild.GetUsersAsync()).First(x => x.Id.ToString() == supremeGamblers[randomSG]);
                if (user != null)
                {
                    if (user.RoleIds.Contains(supremeGamblerRoleId))
                    {
                        await user.RemoveRoleAsync(supremeGamblerRoleId);
                        return user;
                    }
                }
            }
            return null;
        }

        public static async Task<Stream> GetStreamFromUrl(string url)
        {
            byte[] imageData = null;
            using (var wc = new System.Net.WebClient())
            {
                imageData = await wc.DownloadDataTaskAsync(new Uri(url));
            }

            return new MemoryStream(imageData);
        }

        public static async Task SendCooldownMessage(IUser user, IMessageChannel channel)
        {
            await Task.Delay(1000);
            EmbedBuilder gachaEmbed = new EmbedBuilder();
        }

        public static int RemainingStonehearts()
        {
            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\stonehearts.txt");
            int remaining = 10;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != string.Empty)
                    remaining--;
            }
            return remaining;
        }

        public static string FirstObtainerStoneheartPing(int index)
        {
            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\stonehearts.txt");
            if (lines[index] == string.Empty)
                return string.Empty;
            return $"<@{lines[index].Split(' ')[0]}>";
        }

        public static string DupesStoneheartPing(int index)
        {
            string[] lines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\stonehearts.txt");
            string[] split = lines[index].Split(' ');
            Dictionary<string, int> dupes = new Dictionary<string, int>(); 
            string result = string.Empty;
            if (split.Length <= 1)
            {
                return string.Empty;
            }
            for (int i = 1; i < split.Length; i++)
            {
                if (dupes.ContainsKey(split[i]))
                {
                    dupes[split[i]]++;
                } 
                else
                {
                    dupes.Add(split[i], 1);
                }
            }
            if (dupes.Count > 1)
            {
                for (int i = 0; i < dupes.Count - 1; i++)
                {
                    Console.WriteLine("ccc");
                    result += $"<@{dupes.ElementAt(i).Key}> x{dupes.ElementAt(i).Value}, ";
                }
            }
            result += $"<@{dupes.ElementAt(dupes.Count - 1).Key}> x{dupes.ElementAt(dupes.Count - 1).Value}";
            return result;
        }
    }

    public class SlashCommands : InteractionModuleBase
    {
        int buddyzoneCommandsCooldown = 30;
        int maxPulls = 40;

        [SlashCommand("pull", "Do some gambling")]
        public async Task Pull()    
        {
            if (Context.Channel.Id != 1286801789458579497 && Context.Guild.Id == 1171235275020714036)
            {
                await RespondAsync("You can't use this command outside <#1286801789458579497>", ephemeral: true);
                return;
            }
            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(1) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }

            string[] gachaCounterLines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\gachatimeout.txt");
            int pullsDone = 0;

            if (!gachaCounterLines.Any(x => x.Split(' ')[0] == Context.User.Id.ToString()))
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\gachatimeout.txt", true))
                {
                    await writer.WriteLineAsync($"{Context.User.Id.ToString()} {DateTimeOffset.UtcNow.Ticks} 0");
                    writer.Close();
                }
                gachaCounterLines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\gachatimeout.txt");
            }

            for (int i = 0; i < gachaCounterLines.Length; i++)
            {
                string line = gachaCounterLines[i];
                string[] separate = line.Split(' ');
                if (separate[0] == Context.User.Id.ToString())
                {
                    DateTimeOffset lastUsed = new DateTimeOffset(long.Parse(separate[1]), new TimeSpan(0));
                    pullsDone = int.Parse(separate[2]);
                    if (pullsDone >= maxPulls)
                    {
                        if (lastUsed.AddHours(1) >= DateTimeOffset.UtcNow)
                        {
                            await RespondAsync($"Take a break. Your break will end <t:{lastUsed.AddHours(1).ToUnixTimeSeconds()}:R>.", ephemeral: true);
                            return;
                        }
                        else
                        {
                            pullsDone = 0;
                            line = $"{Context.User.Id.ToString()} {DateTimeOffset.UtcNow.Ticks} {pullsDone}";
                            gachaCounterLines[i] = line;
                            System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\gachatimeout.txt", gachaCounterLines);
                        }
                    }
                    break;
                }
            }

            Console.WriteLine($"{Context.User.Username} used /pull in #{Context.Channel.Name} at {DateTimeOffset.UtcNow.ToString()}. They've done {pullsDone++} pulls so far");

            Random random = new Random();
            EmbedBuilder gachaEmbed = new EmbedBuilder();
            EmbedFooterBuilder gachaFooter = new EmbedFooterBuilder();
            gachaFooter.WithText("Used by " + Context.User.Username);
            gachaFooter.WithIconUrl(Context.User.GetAvatarUrl());
            gachaEmbed.WithFooter(gachaFooter);
            try
            {
                if (!System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\skipcutscene.txt").Contains(Context.User.Id.ToString()))
                {
                    gachaEmbed.Color = new Color(random.Next(256), random.Next(256), random.Next(256));
                    gachaEmbed.Title = (Context.User as SocketGuildUser).DisplayName + "'s gambling";
                    gachaEmbed.ImageUrl = "https://media1.tenor.com/m/Cv02PNW6okUAAAAd/star-rail-pull-animation-honkai-star-rail.gif";
                    gachaEmbed.Description = "*Who shall it be?*";
                    await RespondAsync(embed: gachaEmbed.Build());

                    await Task.Delay(7500);
                }
                else
                {
                    gachaEmbed.Color = new Color(random.Next(256), random.Next(256), random.Next(256));
                    gachaEmbed.Title = "Skipping cutscene...";
                    await RespondAsync(embed: gachaEmbed.Build());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await RespondAsync(e.ToString());
            }


            gachaCounterLines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\gachatimeout.txt");
            for (int i = 0; i < gachaCounterLines.Length; i++)
            {
                string line = gachaCounterLines[i];
                string[] separate = line.Split(' ');
                if (separate[0] == Context.User.Id.ToString())
                {
                    DateTimeOffset lastUsed = new DateTimeOffset(long.Parse(separate[1]), new TimeSpan(0));
                    pullsDone = int.Parse(separate[2]);
                    if (pullsDone >= maxPulls)
                    {
                        if (lastUsed.AddHours(1) < DateTimeOffset.UtcNow)
                        {
                            pullsDone = 0;
                            line = $"{Context.User.Id.ToString()} {DateTimeOffset.UtcNow.Ticks} {pullsDone}";
                            gachaCounterLines[i] = line;
                            System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\gachatimeout.txt", gachaCounterLines);
                        }
                    }
                    break;
                }
            }

            GambleResult result = new GambleResult();

            if (AllResults.RemainingStonehearts() > 0)
                result = AllResults.allResults[random.Next(AllResults.allResults.Count)];
            else
                result = AllResults.newResults[random.Next(AllResults.newResults.Count)];

            gachaEmbed = new EmbedBuilder();
            gachaFooter = new EmbedFooterBuilder();
            gachaFooter.WithText("Used by " + Context.User.Username + $" - {maxPulls - pullsDone - 1} pulls left");
            gachaFooter.WithIconUrl(Context.User.GetAvatarUrl());
            gachaEmbed.WithFooter(gachaFooter);

            bool userHasSG = false;

            if (System.IO.File.ReadAllText("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt") != string.Empty)
            {
                userHasSG = System.IO.File.ReadLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt").First() == Context.User.Id.ToString();
            }

            if (random.Next(1000) <= 2)
            {
                int randomStoneheart = random.Next(AllResults.stonehearts.Count);
                if (AllResults.RemainingStonehearts() > 0)
                    randomStoneheart = 2;
                bool firstObtainer = false;
                string[] lines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\stonehearts.txt");
                if (lines[randomStoneheart] == string.Empty)
                {
                    lines[randomStoneheart] = Context.User.Id.ToString();
                    firstObtainer = true;
                } 
                else
                {
                    lines[randomStoneheart] += $" {Context.User.Id}";
                }
                System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\stonehearts.txt", lines);

                gachaEmbed.Title = $"{(Context.User as SocketGuildUser).DisplayName} pulled {AllResults.stonehearts[randomStoneheart].name}";
                gachaEmbed.Color = AllResults.stonehearts[randomStoneheart].color;
                gachaEmbed.ImageUrl = AllResults.stonehearts[randomStoneheart].imageURL;
                if (firstObtainer)
                    gachaEmbed.Description = $"***One of the Ten Stonehearts has been pulled. {AllResults.RemainingStonehearts()} Stonehearts remain.***";
                else
                    gachaEmbed.Description = $"***One of the Ten Stonehearts has been pulled, again. {AllResults.RemainingStonehearts()} Stonehearts still remain... (Dupe)***";

                await ModifyOriginalResponseAsync(msg => msg.Embed = gachaEmbed.Build());

                if (AllResults.RemainingStonehearts() <= 0 && firstObtainer)
                {
                    EmbedBuilder finalStoneheartEmbed = new EmbedBuilder();
                    finalStoneheartEmbed.Title = "All Stonehearts have been collected...";
                    finalStoneheartEmbed.ImageUrl = "https://static.wikia.nocookie.net/houkai-star-rail/images/4/4f/NPC_Diamond.png/revision/latest/scale-to-width-down/1000?cb=20240716082630";
                    finalStoneheartEmbed.Description = "**All Ten Stonehearts have been acquired. With their power, the Supreme Gambler role shall never be revoked, allowing for all of us to be Supreme Gamblers.** " +
                        "\n***Welcome to the next chapter.***";
                    finalStoneheartEmbed.Color = new Color(255, 255, 255);

                    await ReplyAsync(text: "<@&1287418959737655298>", embed: finalStoneheartEmbed.Build());
                }
                else if (firstObtainer)
                    await ReplyAsync(text: "<@&1287418959737655298> a new Stoneheart has been obtained!");
            }/*
            else if (random.Next(100) <= 0 && Context.Guild.Id == 1171235275020714036)
            {
                await AllResults.ReassignSupremeGambler(Context.User, Context.Guild);
                gachaEmbed.Title = $"{(Context.User as SocketGuildUser).DisplayName} has become the Supreme Gambler";
                gachaEmbed.ImageUrl = "https://media1.tenor.com/m/RH8UiFC-aFwAAAAC/aventurine-aventurine-hsr.gif";
                gachaEmbed.Description = "***You are now the new bearer of the Supreme Gambler role***";
                if (userHasSG)
                {
                    gachaEmbed.Title = $"{(Context.User as SocketGuildUser).DisplayName} has become the Supreme Gambler... Again?";
                    gachaEmbed.ImageUrl = "https://media1.tenor.com/m/qDMumw7jgFUAAAAC/aventurine-aventurine-hsr.gif";
                    gachaEmbed.Description = "***Nothing changed... But this sure is a flex.***";
                }
                gachaEmbed.Color = new Color(255, 215, 0);
                await ModifyOriginalResponseAsync(msg => msg.Embed = gachaEmbed.Build());
            }*/
            else if (random.Next(100) <= 0 && Context.Guild.Id == 1171235275020714036 && AllResults.RemainingSupremeGamblers() > 1)
            {
                if (userHasSG)
                {
                    IGuildUser removedUser = null;
                    try
                    {
                        removedUser = await AllResults.RemoveRandomSupremeGambler(Context.User, Context.Guild);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        await RespondAsync("<@904745046329397249> " + e.ToString());
                    }
                    if (removedUser == null)
                    {
                        await RespondAsync("*Something went really wrong! <@904745046329397249>*");
                    }

                    string[] lines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt");
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] == removedUser.Id.ToString())
                        {
                            lines[i] = string.Empty;
                            break;
                        }
                    }
                    System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt", lines);

                    gachaEmbed.Title = $"{removedUser.DisplayName}, one of the Supreme Gamblers, has been defeated.";
                    gachaEmbed.ImageUrl = "https://media1.tenor.com/m/w2vZuAMZ5yQAAAAd/honkai-star-rail-acheron.gif";
                    gachaEmbed.Description = $"***{AllResults.RemainingSupremeGamblers()} Supreme Gamblers remain.***";
                    gachaEmbed.Color = new Color(145, 0, 39);
                    await ModifyOriginalResponseAsync(msg => msg.Embed = gachaEmbed.Build());
                    await ReplyAsync(removedUser.Mention + ", you have lost Supreme Gambler.");

                    /*try
                    {
                        await AllResults.ReassignSupremeGambler(null, Context.Guild);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        await RespondAsync("<@904745046329397249> " + e.ToString());
                    }
                    gachaEmbed.Title = "The Supreme Gambler has been defeated.";
                    gachaEmbed.ImageUrl = "https://media1.tenor.com/m/w2vZuAMZ5yQAAAAd/honkai-star-rail-acheron.gif";
                    gachaEmbed.Description = "***The Supreme Gambler role has been revoked...***";
                    gachaEmbed.Color = new Color(145, 0, 39);
                    await ModifyOriginalResponseAsync(msg => msg.Embed = gachaEmbed.Build()); */
                }
            }
            else if (random.Next(500) <= 2)
            {
                gachaEmbed.Title = (Context.User as SocketGuildUser).DisplayName + $" pulled... {(Context.User as SocketGuildUser).DisplayName}?";
                gachaEmbed.ImageUrl = (Context.User as SocketGuildUser).GetDisplayAvatarUrl();
                gachaEmbed.Description = "It's you!";
                gachaEmbed.Color = new Color(random.Next(255), random.Next(255), random.Next(255));
                await ModifyOriginalResponseAsync(msg => msg.Embed = gachaEmbed.Build());
            }
            else
            {
                gachaEmbed.Title = (Context.User as SocketGuildUser).DisplayName + " pulled " + result.name;
                gachaEmbed.ImageUrl = result.imageURL;
                gachaEmbed.Description = result.reaction;
                gachaEmbed.Color = result.color;
                await ModifyOriginalResponseAsync(msg => msg.Embed = gachaEmbed.Build());
            }

            for (int i = 0; i < gachaCounterLines.Length; i++)
            {
                string line = gachaCounterLines[i];
                string[] separate = line.Split(' ');
                if (separate[0] == Context.User.Id.ToString())
                {
                    DateTimeOffset lastUsed = new DateTimeOffset(long.Parse(separate[1]), new TimeSpan(0));
                    pullsDone = int.Parse(separate[2]);
                    if (pullsDone > maxPulls)
                    {
                        if (lastUsed.AddHours(1) >= DateTimeOffset.UtcNow)
                        {
                            await RespondAsync($"Take a break. Your break will end <t:{lastUsed.AddHours(1).ToUnixTimeSeconds()}:R>.", ephemeral: true);
                            return;
                        }
                        else
                        {
                            pullsDone = 1;
                            line = $"{Context.User.Id.ToString()} {DateTimeOffset.UtcNow.Ticks} {pullsDone}";
                        }
                    }
                    else
                    {
                        if (lastUsed.AddHours(1) >= DateTimeOffset.UtcNow)
                        {
                            pullsDone++;
                            line = $"{Context.User.Id.ToString()} {DateTimeOffset.UtcNow.Ticks} {pullsDone}";
                        }
                        else
                        {
                            pullsDone = 1;
                            line = $"{Context.User.Id.ToString()} {DateTimeOffset.UtcNow.Ticks} {pullsDone}";
                        }
                    }
                    gachaCounterLines[i] = line;
                    System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\gachatimeout.txt", gachaCounterLines);
                    break;
                }
            }

            if (pullsDone >= maxPulls)
            {
                gachaEmbed.Title = $"Take a break, {(Context.User as SocketGuildUser).DisplayName}.";
                gachaEmbed.ImageUrl = "https://media1.tenor.com/m/HItBOocy6ikAAAAC/umaru-sleeping.gif";
                gachaEmbed.Description = "You can't pull for an hour. Gambling addiction is bad.";
                gachaEmbed.Color = new Color(74, 81, 99);
                gachaEmbed.Footer = null;
                await ReplyAsync(embed: gachaEmbed.Build());
            }
        }

        [SlashCommand("stonehearts", "Show the currently obtained Stonehearts")]
        public async Task Stonehearts()
        {
            /* await RespondAsync("🤫", ephemeral: true);
            return; */
            Console.WriteLine($"{Context.User.Username} used /stonehearts in {Context.Channel.Name}. Remaining stonehearts: {AllResults.RemainingStonehearts()}");
            try
            {
                EmbedBuilder stoneheartEmbed = new EmbedBuilder();
                stoneheartEmbed.Title = "The Ten Stonehearts";
                Random random = new Random();
                stoneheartEmbed.Color = AllResults.stonehearts[random.Next(AllResults.stonehearts.Count)].color;
                EmbedFieldBuilder field = new EmbedFieldBuilder();
                for (int i = 0; i < AllResults.stonehearts.Count; i++)
                {
                    field = new EmbedFieldBuilder();
                    field.Name = AllResults.stonehearts[i].name;
                    if (AllResults.FirstObtainerStoneheartPing(i) == string.Empty)
                    {
                        field.Value = "***Unobtained***";
                    }
                    else if (AllResults.DupesStoneheartPing(i) == string.Empty)
                    {
                        field.Value = $"**Obtained by:** {AllResults.FirstObtainerStoneheartPing(i)}";
                    }
                    else
                    {
                        field.Value = $"**Obtained by:** {AllResults.FirstObtainerStoneheartPing(i)}\n" +
                            $"Dupes: {AllResults.DupesStoneheartPing(i)}";
                    }
                    stoneheartEmbed.AddField(field);
                }
                await RespondAsync(embed: stoneheartEmbed.Build());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await RespondAsync(e.ToString());
            }
        }

        [SlashCommand("supremegamblers", "Show the current supreme gamblers")]
        public async Task SupremeGamblers()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\supremegambler.txt");
                string result = string.Empty;
                foreach (string line in lines)
                {
                    result += $"<@{line}>\n";
                }
                await RespondAsync(result, ephemeral: true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await RespondAsync(e.ToString());
            }
        }

        [SlashCommand("suggest", "Suggest a pull!")]
        public async Task Suggest(string name, IAttachment image, string description)
        {
            if (image.ContentType.Split('/')[0] != "image")
            {
                await RespondAsync("Please put in an image", ephemeral: true);
                return;
            }
            try
            {
                IGuildUser beanie = await Context.Guild.GetUserAsync(904745046329397249);
                IMessageChannel beanieChannel = await beanie.CreateDMChannelAsync();
                EmbedBuilder embed = new EmbedBuilder();
                embed.Title = $"{(Context.User as SocketGuildUser).DisplayName} suggested {name}";
                embed.Description = description;
                embed.ImageUrl = image.Url;
                Random random = new Random();
                embed.Color = new Color(random.Next(255), random.Next(255), random.Next(255));
                await beanieChannel.SendMessageAsync(embed: embed.Build());
                await RespondAsync("Suggested!", ephemeral: true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await RespondAsync(e.ToString(), ephemeral: true);
            }
        }

        [SlashCommand("kiss", "Kiss a member")]
        public async Task Kiss(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /kiss in #{Context.Channel.Name}");

            Random random = new Random();
            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = new Color(255, 120, 120);
            embedToSend.Description = Context.User.Mention + " kisses " + user.Mention;
            embedToSend.ImageUrl = AllResults.kissGifs[random.Next(AllResults.kissGifs.Length)];

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }

        [SlashCommand("hug", "Hug a member")]
        public async Task Hug(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /hug in #{Context.Channel.Name}");

            Random random = new Random();
            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = Color.Blue;
            embedToSend.Description = Context.User.Mention + " hugs " + user.Mention;
            embedToSend.ImageUrl = AllResults.hugGifs[random.Next(AllResults.hugGifs.Length)];

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }
        [SlashCommand("punch", "Punch a member")]
        public async Task Punch(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /punch in #{Context.Channel.Name}");

            Random random = new Random();
            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = Color.Red;
            if (random.Next(5) == 0)
            {
                embedToSend.Description = "*" + Context.User.Mention + " tried to punch " + user.Mention + ", but completely missed.*";
                await RespondAsync(embed: embedToSend.Build());
                return;
            }
            if (AllResults.punchCounter == 24 || (AllResults.punchCounter == 0 && random.Next(10) == 0))
            {
                embedToSend.Description = Context.User.Mention + " uses Black Flash on " + user.Mention;
                embedToSend.ImageUrl = "https://cdn.discordapp.com/attachments/1285365379438477403/1285389220168597504/itadori-itadori-yuji.gif?ex=66ea177f&is=66e8c5ff&hm=0c88bb6ebdaeb1fcc36fe7c6358fe6a34912d6559d63914e5fbfca3bad6ebaa0&";

                await RespondAsync(embed: embedToSend.Build());
                AllResults.punchCounter = 0;
                return;
            }
            embedToSend.Description = Context.User.Mention + " punches " + user.Mention;
            embedToSend.ImageUrl = AllResults.punchGifs[random.Next(AllResults.punchGifs.Length)];
            AllResults.punchCounter++;
            Console.WriteLine("Current punch: " + AllResults.punchCounter);

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }

        [SlashCommand("headpat", "Headpat a member")]
        public async Task Headpat(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /headpat in #{Context.Channel.Name}");

            Random random = new Random();
            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = Color.Orange;
            embedToSend.Description = Context.User.Mention + " headpats " + user.Mention;
            embedToSend.ImageUrl = AllResults.patGifs[random.Next(AllResults.patGifs.Length)];

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }

        [SlashCommand("throw", "Throw a member")]
        public async Task Throw(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /throw in #{Context.Channel.Name}");

            Random random = new Random();

            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = Color.Teal;
            if (random.Next(5) == 0)
            {
                embedToSend.Description = "*" + Context.User.Mention + " tried to throw " + user.Mention + ", but failed miserably.*";
                await RespondAsync(embed: embedToSend.Build());
                return;
            }
            embedToSend.Description = Context.User.Mention + " throws " + user.Mention;
            embedToSend.ImageUrl = AllResults.throwGifs[random.Next(AllResults.throwGifs.Length)];

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }

        [SlashCommand("stab", "Stab a member")]
        public async Task Stab(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /stab in #{Context.Channel.Name}");

            Random random = new Random();
            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = Color.DarkRed;
            if (random.Next(5) == 0)
            {
                embedToSend.Description = "*" + Context.User.Mention + " tried to stab " + user.Mention + ", but missed. Loser.*";
                await RespondAsync(embed: embedToSend.Build());
                return;
            }
            embedToSend.Description = Context.User.Mention + " stabs " + user.Mention;
            embedToSend.ImageUrl = AllResults.stabGifs[random.Next(AllResults.stabGifs.Length)];

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }

        [SlashCommand("slap", "Slap a member")]
        public async Task Slap(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /slap in #{Context.Channel.Name}");

            Random random = new Random();
            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = Color.Magenta;
            if (random.Next(5) == 0)
            {
                embedToSend.Description = "*" + Context.User.Mention + " tried to slap " + user.Mention + ", but couldn't hit them.*";
                await RespondAsync(embed: embedToSend.Build());
                return;
            }
            embedToSend.Description = Context.User.Mention + " slaps " + user.Mention;
            embedToSend.ImageUrl = AllResults.slapGifs[random.Next(AllResults.slapGifs.Length)];

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }

        [SlashCommand("bite", "Bite a member")]
        public async Task Bite(IUser user)
        {
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(buddyzoneCommandsCooldown) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /bite in #{Context.Channel.Name}");

            Random random = new Random();
            EmbedBuilder embedToSend;
            embedToSend = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embedToSend.WithFooter(embedFooterBuilder);
            embedToSend.Color = Color.Purple;
            embedToSend.Description = Context.User.Mention + " bites " + user.Mention;
            embedToSend.ImageUrl = AllResults.biteGifs[random.Next(AllResults.biteGifs.Length)];

            await RespondAsync(text: user.Mention, embed: embedToSend.Build());
        }

        [SlashCommand("fate", "Show your fate with a member")]
        public async Task Fate(IUser user)
        {
            if (Context.Channel.Id == 1171251045947682876)
            {
                await RespondAsync("You can't use this command in #buddy-zone", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
                return;
            }

            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString()))
            {
                await RespondAsync("This user has opted out of the bot.", ephemeral: true);
            }

            if (AllResults.cooldowns.ContainsKey(Context.User as SocketGuildUser))
            {
                if (AllResults.cooldowns[Context.User as SocketGuildUser].AddSeconds(5) >= DateTimeOffset.UtcNow)
                {
                    await RespondAsync("You're using the bot too often!", ephemeral: true);
                    return;
                }
                else
                {
                    AllResults.cooldowns[Context.User as SocketGuildUser] = DateTimeOffset.UtcNow;
                }
            }
            else
            {
                AllResults.cooldowns.Add(Context.User as SocketGuildUser, DateTimeOffset.UtcNow);
            }
            Console.WriteLine($"{Context.User.Username} used /fate in #{Context.Channel.Name}");

            string[] responses = AllResults.relationships;
            string[] nsfwResponses = AllResults.relationships.Concat(AllResults.nsfwRelationships).ToArray();

            bool contextUserOptedIn = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt").Contains(Context.User.Id.ToString());
            bool contextUserOptedInWithTarget = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt").Contains(Context.User.Id.ToString() + " " + user.Id.ToString());
            bool targetOptedIn = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt").Contains(user.Id.ToString());
            bool targetOptedInWithContextUser = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt").Contains(user.Id.ToString() + " " + Context.User.Id.ToString());

            if (targetOptedIn && contextUserOptedIn)
            {
                responses = nsfwResponses;
            }
            else if (targetOptedIn && contextUserOptedInWithTarget)
            {
                responses = nsfwResponses;
            }
            else if (targetOptedInWithContextUser && contextUserOptedIn)
            {
                responses = nsfwResponses;
            }
            else if (targetOptedInWithContextUser && contextUserOptedInWithTarget)
            {
                responses = nsfwResponses;
            }

            EmbedBuilder embed = new EmbedBuilder();

            Random random = new Random();
            if (random.Next(500) <= 2)
            {
                embed.Color = Color.Red;
                embed.ImageUrl = "https://m.media-amazon.com/images/M/MV5BOTg5ZjI5ZTAtOWE2OS00MjY4LWI4ZDQtMzEzMDY4MTgzYWJlXkEyXkFqcGdeQXVyNjc3OTE4Nzk@._V1_FMjpg_UX1000_.jpg";
                await RespondAsync(embed: embed.Build());
                return;
            }

            Random relationshipRandom = new Random((int)(user.Id % 1000000) + (int)(Context.User.Id % 1000000));

            embed = new EmbedBuilder();
            EmbedFooterBuilder embedFooterBuilder = new EmbedFooterBuilder();
            embedFooterBuilder.WithText("Used by " + Context.User.Username);
            embedFooterBuilder.WithIconUrl(Context.User.GetAvatarUrl());
            embed.WithFooter(embedFooterBuilder);
            embed.Color = new Color(relationshipRandom.Next(256), relationshipRandom.Next(256), relationshipRandom.Next(256));
            if ((Context.User.Username == "march7thfan_" && user.Username == "seelesrightboob")
                || (Context.User.Username == "seelesrightboob" && user.Username == "march7thfan_"))
            {
                embed.Title = "Cyno and Sora are dating";
                embed.Description = "...in every universe";
            }
            else
            {
                string finalString = responses[relationshipRandom.Next(responses.Length)];
                finalString = Regex.Replace(finalString, "USER_1", (Context.User as SocketGuildUser).DisplayName);
                finalString = Regex.Replace(finalString, "USER_2", (user as SocketGuildUser).DisplayName);
                embed.Title = finalString;
            }
            embed.Description = "...in most parallel worlds";

            await RespondAsync(embed: embed.Build());
        }

        [SlashCommand("skipcutscene", "Allows you to skip the /pull cutscene with Pom-Pom")]
        public async Task SkipCutscene(bool skip)
        {
            Console.WriteLine($"{Context.User.Username} used /skipcutscene {skip} in #{Context.Channel.Name}");
            string fullId = Context.User.Id.ToString();
            if (skip)
            {
                if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\skipcutscene.txt").Contains(fullId))
                {
                    await RespondAsync("*You are already opted out of the cutscene.*", ephemeral: true);
                }
                else
                {
                    using (StreamWriter file = new StreamWriter("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\skipcutscene.txt", true))
                    {
                        try
                        {
                            await file.WriteLineAsync(fullId);
                            file.Close();
                            await RespondAsync("*You have been opted out of the cutscene.*", ephemeral: true);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            await RespondAsync(e.ToString(), ephemeral: true);
                        }
                    }
                }
            }
            else
            {
                if (!System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\skipcutscene.txt").Contains(fullId))
                {
                    await RespondAsync("*You are already opted in to the cutscene.*", ephemeral: true);
                }
                else
                {
                    var newLines = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\skipcutscene.txt").Select(line => Regex.Replace(line, $"^{fullId}\\s*$", string.Empty, RegexOptions.IgnoreCase));
                    System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\skipcutscene.txt", newLines);
                    await RespondAsync("*You have been opted in to the cutscene.*", ephemeral: true);
                }
            }
        }

        [SlashCommand("sgicon", "Change the icon of the Supreme Gambler role, if you have it")]
        public async Task SGIcon(IAttachment attachment)
        {
            if (attachment.ContentType.Split('/')[0] != "image")
            {
                await RespondAsync("Please put in an image", ephemeral: true);
                return;
            }
            IRole role = null;
            try
            {
                role = await Context.Guild.GetRoleAsync(1285960546365214832);
            }
            catch (Exception e)
            {
                await RespondAsync(e.ToString());
            }
            if ((Context.User as SocketGuildUser).Roles.Any(r => r.Id == 1285960546365214832))
            {
                try
                {
                    Stream imageStream = await AllResults.GetStreamFromUrl(attachment.Url);
                    Image iconImage = new Image(imageStream);
                    await role.ModifyAsync(r => r.Icon = iconImage);
                    await RespondAsync("Changed", ephemeral: true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    await RespondAsync(e.ToString());
                }
            }
            else
            {
                await RespondAsync("You are not the Supreme Gambler.", ephemeral: true);
            }
        }

        [SlashCommand("roll", "Roll a die, d20 by default")]
        public async Task Roll(string dice = "d20")
        {
            Random random = new Random();
            string[] split = dice.ToLower().Split(new string[] { "d" }, StringSplitOptions.RemoveEmptyEntries);
            int amount = 1;
            int diceNumber;
            if (split.Length < 2)
            {
                Console.WriteLine(split[0]);
                if (!int.TryParse(split[0], out diceNumber))
                {
                    await RespondAsync("Please enter a valid dice", ephemeral: true);
                }
            }
            else
            {
                Console.WriteLine(split[0]);
                Console.WriteLine(split[1]);
                if (!int.TryParse(split[0], out amount))
                {
                    await RespondAsync("Please enter a valid dice", ephemeral: true);
                }
                if (!int.TryParse(split[1], out diceNumber))
                {
                    await RespondAsync("Please enter a valid dice", ephemeral: true);
                }
            }
            int sum = 0;
            string rolls = string.Empty;
            for (int i = 0; i < amount; i++)
            {
                int randomNumber = random.Next(diceNumber) + 1;
                rolls += $"{randomNumber} ";
                sum += randomNumber;
            }
            EmbedBuilder rollEmbed = new EmbedBuilder();
            if (dice.Contains('d'))
                rollEmbed.Title = $"{(Context.User as SocketGuildUser).DisplayName} rolling {dice}:";
            else
                rollEmbed.Title = $"{(Context.User as SocketGuildUser).DisplayName} rolling d{dice}:";
            rollEmbed.Description = $"{rolls}\n**Total: {sum}**";
            rollEmbed.Color = new Color(random.Next(255), random.Next(255), random.Next(255));
            await RespondAsync(embed: rollEmbed.Build());
        }

        [SlashCommand("test", "Test command")]
        public async Task Test()
        {
            await RespondAsync("nothing to test rn", ephemeral: true);
            return;
            string[] lines = new string[9];
            System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\stonehearts.txt", lines);
            await RespondAsync("check it", ephemeral: true);
        }

        [SlashCommand("optout", "Prevent others from using the bot with you")]
        public async Task OptOut(IUser user = null)
        {
            Console.WriteLine($"{Context.User.Username} used /optout in #{Context.Channel.Name}");
            string fullId = Context.User.Id.ToString();
            if (user != null)
            {
                fullId += " " + user.Id.ToString();
            }
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(fullId))
            {
                if (user == null)
                    await RespondAsync("*You are already opted out.*", ephemeral: true);
                else
                    await RespondAsync("*You've already opted out with that user.*", ephemeral: true);
            }
            else
            {
                using (StreamWriter file = new StreamWriter("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt", true))
                {
                    try
                    {
                        await file.WriteLineAsync(fullId);
                        file.Close();
                        AllResults.blacklist = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt");
                        if (user == null)
                            await RespondAsync("*You have been opted out.*", ephemeral: true);
                        else
                            await RespondAsync("*You have been opted out with that user.*", ephemeral: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        await RespondAsync(e.ToString(), ephemeral: true);
                    }
                }
            }
        }

        [SlashCommand("optin", "Allow others to use the bot with you")]
        public async Task OptIn(IUser user = null)
        {
            Console.WriteLine($"{Context.User.Username} used /optin in #{Context.Channel.Name}");
            string fullId = Context.User.Id.ToString();
            if (user != null)
            {
                fullId += " " + user.Id.ToString();
            }
            if (!System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt").Contains(fullId))
            {
                if (user == null)
                    await RespondAsync("*You are already opted in.*", ephemeral: true);
                else
                    await RespondAsync("*You've already opted in with that user.*", ephemeral: true);
            }
            else
            {
                var newLines = AllResults.blacklist.Select(line => Regex.Replace(line, $"^{fullId}\\s*$", string.Empty, RegexOptions.IgnoreCase));
                System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt", newLines);
                AllResults.blacklist = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\blacklist.txt");
                await RespondAsync("*You have been opted in.*", ephemeral: true);
            }
        }

        [SlashCommand("nsfwoptin", "Allow NSFW results to appear in /fate for you")]
        public async Task NSFWOptIn(IUser user = null)
        {
            Console.WriteLine($"{Context.User.Username} used /nsfwoptin in #{Context.Channel.Name}");
            string fullId = Context.User.Id.ToString();
            if (user != null)
            {
                fullId += " " + user.Id.ToString();
            }
            if (System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt").Contains(fullId))
            {
                if (user == null)
                    await RespondAsync("*You are already opted in.*", ephemeral: true);
                else
                    await RespondAsync("*You've already opted in with that user.*", ephemeral: true);
            }
            else
            {
                using (StreamWriter file = new StreamWriter("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt", true))
                {
                    try
                    {
                        await file.WriteLineAsync(fullId);
                        file.Close();
                        if (user == null)
                            await RespondAsync("*You have been opted in.*", ephemeral: true);
                        else
                            await RespondAsync("*You have been opted in with that user.*", ephemeral: true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        await RespondAsync(e.ToString(), ephemeral: true);
                    }
                }
            }
        }

        [SlashCommand("nsfwoptout", "Prevent NSFW results to appear in /fate for you")]
        public async Task NSFWOptOut(IUser user = null)
        {
            Console.WriteLine($"{Context.User.Username} used /nsfwoptout in #{Context.Channel.Name}");
            string fullId = Context.User.Id.ToString();
            if (user != null)
            {
                fullId += " " + user.Id.ToString();
            }
            if (!System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt").Contains(fullId))
            {
                if (user == null)
                    await RespondAsync("*You are already opted out.*", ephemeral: true);
                else
                    await RespondAsync("*You've already opted out with that user.*", ephemeral: true);
            }
            else
            {
                var whitelist = System.IO.File.ReadAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt");
                var newLines = whitelist.Select(line => Regex.Replace(line, $"^{fullId}\\s*$", string.Empty, RegexOptions.IgnoreCase));
                System.IO.File.WriteAllLines("C:\\Users\\kuzzz\\source\\repos\\ScrewllumBot\\ScrewllumBot\\nsfwoptin.txt", newLines);
                await RespondAsync("*You have been opted out.*", ephemeral: true);
            }
        }
    }
}
