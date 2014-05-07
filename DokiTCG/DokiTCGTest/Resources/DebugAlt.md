Rizon connect log
=================

```
-> irc.rizon.sexy QUIT :
-> irc.rizon.net CAP LS
-> irc.rizon.net NICK Passion
-> irc.rizon.net USER Setsuna 0 * :Setsuna

<- :irc.sxci.net 439 * :Please wait while we process your connection.
<- :irc.sxci.net NOTICE AUTH :*** Looking up your hostname...
<- :irc.sxci.net NOTICE AUTH :*** Checking Ident
<- :irc.sxci.net NOTICE AUTH :*** No Ident response
<- :irc.sxci.net NOTICE AUTH :*** Found your hostname
<- :irc.sxci.net CAP * LS :multi-prefix uhnames userhost-in-names

-> irc.rizon.net CAP REQ :multi-prefix

<- :irc.sxci.net CAP Passion ACK :multi-prefix

-> irc.rizon.net CAP END

<- :irc.sxci.net NOTICE Passion :*** Your host is masked (Rizon-BE28B886.direct-adsl.nl)
<- :irc.sxci.net 001 Passion :Welcome to the Rizon Internet Relay Chat Network Passion

-> irc.sxci.net USERHOST Passion

<- :irc.sxci.net 002 Passion :Your host is irc.sxci.net[208.100.38.65/6662], running version hybrid-7.2.3+plexus-3.1.0(20130128_0-537)
<- :irc.sxci.net 003 Passion :This server was created Jan 28 2013 at 13:15:41
<- :irc.sxci.net 004 Passion irc.sxci.net hybrid-7.2.3+plexus-3.1.0(20130128_0-537) CDFGNRSUWXabcdfgijklnopqrsuwxyz BIMNORSabcehiklmnopqstvz Iabehkloqv
<- :irc.sxci.net 005 Passion CALLERID CASEMAPPING=rfc1459 DEAF=D KICKLEN=160 MODES=4 NICKLEN=30 TOPICLEN=390 PREFIX=(qaohv)~&@%+ STATUSMSG=~&@%+ NETWORK=Rizon MAXLIST=beI:250 TARGMAX=ACCEPT:,KICK:1,LIST:1,NAMES:1,NOTICE:4,PRIVMSG:4,WHOIS:1 CHANTYPES=# :are supported by this server
<- :irc.sxci.net 005 Passion CHANLIMIT=#:75 CHANNELLEN=50 CHANMODES=beI,k,l,BCMNORScimnpstz FNC ELIST=CMNTU SAFELIST NAMESX UHNAMES AWAYLEN=160 KNOCK EXCEPTS=e INVEX=I :are supported by this server
<- :irc.sxci.net 042 Passion 08CACDGXY :your unique ID
<- :irc.sxci.net 251 Passion :There are 31 users and 19249 invisible on 19 servers
<- :irc.sxci.net 252 Passion 76 :IRC Operators online
<- :irc.sxci.net 253 Passion 910 :unknown connection(s)
<- :irc.sxci.net 254 Passion 21883 :channels formed
<- :irc.sxci.net 255 Passion :I have 19280 clients and 0 servers
<- :irc.sxci.net 265 Passion 19280 26117 :Current local users 19280, max 26117
<- :irc.sxci.net 266 Passion 19280 26117 :Current global users 19280, max 26117
<- :irc.sxci.net 375 Passion :- irc.sxci.net Message of the Day - 
<- :irc.sxci.net 372 Passion :-          11oo                            
<- :irc.sxci.net 372 Passion :- 
<- :irc.sxci.net 372 Passion :- 88d888b. dP d888888b .d8888b. 88d888b. 
<- :irc.sxci.net 372 Passion :- 88'  `88 88    .d8P' 88'  `88 88'  `88 
<- :irc.sxci.net 372 Passion :- 1188       88  .Y8P    88.  .88 88    88 
<- :irc.sxci.net 372 Passion :- 11dP       dP d888888P `88888P' dP    dP
<- :irc.sxci.net 372 Passion :- 
<- :irc.sxci.net 372 Passion :- Rizon Chat Network -- http://rizon.net
<- :irc.sxci.net 372 Passion :- 
<- :irc.sxci.net 372 Passion :- Listening on ports: 6660 - 6669, 7000. SSL: 6697, 9999
<- :irc.sxci.net 372 Passion :- 
<- :irc.sxci.net 372 Passion :- Rules:
<- :irc.sxci.net 372 Passion :- o No spamming or flooding
<- :irc.sxci.net 372 Passion :- o No clones or malicious bots
<- :irc.sxci.net 372 Passion :- o No takeovers
<- :irc.sxci.net 372 Passion :- o No distribution of child pornography
<- :irc.sxci.net 372 Passion :- o Clients must respond to VERSION requests
<- :irc.sxci.net 372 Passion :- o Rizon staff may disconnect clients for any or no reason
<- :irc.sxci.net 372 Passion :- 
<- :irc.sxci.net 372 Passion :- First steps:
<- :irc.sxci.net 372 Passion :- o To register your nick: /msg NickServ HELP
<- :irc.sxci.net 372 Passion :- o To register your channel: /msg ChanServ HELP
<- :irc.sxci.net 372 Passion :- o To get a vHost: /msg HostServ HELP REQUEST
<- :irc.sxci.net 372 Passion :- o For other help with Rizon: /join #help
<- :irc.sxci.net 372 Passion :- 
<- :irc.sxci.net 372 Passion :- Usage of this network is a privilege, not a right. Rizon is a
<- :irc.sxci.net 372 Passion :- transit provider, therefore no person or entity involved with
<- :irc.sxci.net 372 Passion :- *.rizon.net or irc.sxci.net takes any responsibility for
<- :irc.sxci.net 372 Passion :- users' actions. Absolutely no warranty is expressed or implied.
<- :irc.sxci.net 376 Passion :End of /MOTD command.
<- :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl MODE Passion :+ix
<- :py-ctcp!ctcp@ctcp-scanner.rizon.net PRIVMSG Passion :VERSION

-> irc.sxci.net NOTICE py-ctcp :VERSION mIRC v7.32 Khaled Mardam-Bey

<- :irc.sxci.net 302 Passion :Passion=+~Setsuna@81.207.88.178 
<- :Global!service@rizon.net NOTICE Passion :[Logon News - May 21 2011] First time on Rizon? Be sure to read the FAQ! http://s.rizon.net/FAQ
<- :Global!service@rizon.net NOTICE Passion :[Logon News - Dec 16 2013] Own a large/active channel or plan to get one going? Please read http://s.rizon.net/authline
<- :Global!service@rizon.net NOTICE Passion :[Random News - Mar 20 2009] Idle on Rizon a lot? Why not play our idlerpg game, you can check it out at #RizonIRPG for more information visit the website http://idlerpg.rizon.net -Rizon Staff
<- :NickServ!service@rizon.net NOTICE Passion :This nickname is registered and protected. If it is your
<- :NickServ!service@rizon.net NOTICE Passion :nick, type /msg NickServ IDENTIFY password. Otherwise,
<- :NickServ!service@rizon.net NOTICE Passion :please choose a different nick.

-> irc.sxci.net PROTOCTL NAMESX
-> irc.sxci.net PROTOCTL UHNAMES
-> irc.sxci.net JOIN #doki-development,#Severin,#doki-tcg,#doki-tcg-arena

<- :peer!service@rizon.net NOTICE Passion :For network safety, your client is being scanned for open proxies by scanner.rizon.net (80.65.51.219). This scan will not harm your computer.
<- :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl JOIN :#doki-development

-> irc.sxci.net MODE #doki-development

<- :irc.sxci.net 332 Passion #doki-development :<4!> 7FOREVER UNDER CONSTRUCTION <4!> |
<- :irc.sxci.net 333 Passion #doki-development TheThing|24-7!Regina@dokidoki.pre.cure 1396977957
<- :irc.sxci.net 353 Passion @ #doki-development :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl %Chitoge-chan!~chitoge@kirisaki.chitoge &@altazure!altazure@jitsugi.wa.nigate.desu.ga.bunseki.wa.tokui.desu KamiyamaKiriko!~K@miyama.Kiriko %thingy-kun!thingy-kun@the.thing.connecting.dimensi.on &@+kb_z!kb_z@Nisi.O.isiN ~@Orillion!Orillion@Doki.Fansubs Holo|Lab!~misterano@Doki.Fansubs &@TheThing|24-7!Regina@dokidoki.pre.cure &@Kanbaru_Suruga!sen-chan@Nisi.O.isiN @Internets!internet@services.rizon.net
<- :irc.sxci.net 353 Passion @ #doki-development :@Quotes!quotes@rizon.net &@Harbinger!Severin@Doki.Services &@DeathHere!~DeathHere@Eat.My.Railgun %Saki-chan!~Saki@The.Nationals &@[Doki]!Doki@Doki.Fansubs
<- :irc.sxci.net 366 Passion #doki-development :End of /NAMES list.
<- :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl JOIN :#Severin

-> irc.sxci.net MODE #Severin

<- :irc.sxci.net 332 Passion #Severin :11~8â¿11~ 14[13 Mariko-sama is the queen 14] 11~8â¿11~ 14[13 sakurahime is the princess 14] 11~8â¿11~ 14[13 Orillion is the Field Marshal 14] 11~8â¿11~
<- :irc.sxci.net 333 Passion #Severin Mariko-sama!~Mimori@Akiba.Star 1395703379
<- :irc.sxci.net 353 Passion @ #Severin :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl @sakurahime!~myhime~@hime.chan.desu &@Harbinger!Severin@Doki.Services +Orillion!Orillion@Doki.Fansubs &@TheKey!services@services.rizon.net
<- :irc.sxci.net 366 Passion #Severin :End of /NAMES list.
<- :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl JOIN :#doki-tcg

-> irc.sxci.net MODE #doki-tcg

<- :irc.sxci.net 353 Passion = #doki-tcg :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl &@[Doki]!Doki@Doki.Fansubs ~@Harbinger!Severin@Doki.Services %Orillion!Orillion@Doki.Fansubs
<- :irc.sxci.net 366 Passion #doki-tcg :End of /NAMES list.
<- :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl JOIN :#doki-tcg-arena

-> irc.sxci.net MODE #doki-tcg-arena

<- :irc.sxci.net 353 Passion = #doki-tcg-arena :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl %Orillion!Orillion@Doki.Fansubs &@[Doki]!Doki@Doki.Fansubs &@Harbinger!Severin@Doki.Services
<- :irc.sxci.net 366 Passion #doki-tcg-arena :End of /NAMES list.
<- :Passion!~Setsuna@Rizon-BE28B886.direct-adsl.nl MODE Passion :+r
<- :NickServ!service@rizon.net NOTICE Passion :Password accepted - you are now recognized.
<- :HostServ!service@rizon.net NOTICE Passion :Your vhost of Laby.rinth is now activated.
<- :[Doki]!Doki@Doki.Fansubs MODE #doki-development +h Passion
<- :TheKey!services@services.rizon.net MODE #Severin +h Passion
<- :[Doki]!Doki@Doki.Fansubs MODE #doki-tcg +ao Passion Passion

-> irc.sxci.net MODE #doki-tcg

<- :[Doki]!Doki@Doki.Fansubs MODE #doki-tcg-arena +ao Passion Passion

-> irc.sxci.net MODE #doki-tcg-arena

<- :irc.sxci.net 324 Passion #doki-development +nstz 
<- :irc.sxci.net 329 Passion #doki-development 1387548652
<- :irc.sxci.net 324 Passion #Severin +inpstz 
<- :irc.sxci.net 329 Passion #Severin 1392902658
<- :irc.sxci.net 324 Passion #doki-tcg +ntz 
<- :irc.sxci.net 329 Passion #doki-tcg 1399283476
<- :irc.sxci.net 324 Passion #doki-tcg-arena +mntz 
<- :irc.sxci.net 329 Passion #doki-tcg-arena 1399296052
<- :irc.sxci.net 324 Passion #doki-tcg +ntz 
<- :irc.sxci.net 329 Passion #doki-tcg 1399283476
<- :irc.sxci.net 324 Passion #doki-tcg-arena +mntz 
<- :irc.sxci.net 329 Passion #doki-tcg-arena 1399296052
<- PING :irc.sxci.net

-> irc.sxci.net PONG :irc.sxci.net

-> irc.shakeababy.net MODE #doki-development +o Passion
<- :Orillion!Orillion@Doki.Fansubs MODE #doki-development +o Passion

-> irc.shakeababy.net NICK :Orillion

-> irc.shakeababy.net PART #Severin
-> irc.shakeababy.net JOIN #Severin
<- :Orillion!Orillion@Doki.Fansubs PART #Severin
<- :Orillion!Orillion@Doki.Fansubs JOIN :#Severin
```