import player
import board
import array
import pygame
import sys
import math
import random

class Game(object):

    # zTable to hold info
    zTable = [[[11578305893031676234, 5570717585647348436], [3966369782194981892, 8357864058275782850], [8625207003216349124, 10698175357680835627], [18416312903974663339, 5714949458850273512], [4944773223431107849, 13429660142847485009], [15763469952518396229, 15455555846909119012], [6916078478016769752, 371947745149887214], [16509064259898519984, 13819611394432008820]],
              [[13037978171632197094, 2307228024421067716], [9105831359883914668, 12489467957059304894], [2921879770588316964, 12398139783255467067], [15079598618700715866, 18296175503612019597], [13280792867528028804, 13221759859338596840], [5652634036926569863, 12862732065780499613], [12055634958255084544, 6750899573511714776], [13216403536898832102, 13036594057595654795]],
              [[5054748102380919980, 16468922479202382206], [9551144798393501326, 3128178764413265193], [1629622401726723629, 3378600696184710921], [10307826817703160718, 11713219260829356742], [17104151671350197820, 15030966334723391094], [11388188092627339914, 4215784011639378988], [14222850309498037843, 7176940794708046330], [2968320975291260027, 17971895649925825662]],
              [[11053423098328122916, 15915482358358171058], [2671896062460379927, 4925902693266613910], [11634125168481922832, 2355194901547588599], [2401004146964148884, 6902924576623462064], [10124882305921993809, 9401776917186919589], [9508615448425881811, 12645432617840776142], [8710185733430428465, 11790927118342421381], [3526716574661898413, 5554452988087524454]],
              [[7780069388176897126, 14800428383399665092], [15466101403103388054, 1768180300686537707], [9524401931988086819, 136355275128643215], [7984458451479473181, 339733902926524074], [6649910553077626943, 17395107967761611848], [10320673640031219927, 18377887974348280417], [137813707314700940, 18169449616661557469], [1825492388098893382, 6236765581759408302]],
              [[1038678869107750799, 3474084905236259863], [3514999513861837683, 9077467443470889601], [8090561760027790509, 11163437210294593784], [5794060612055405161, 13294439838732963862], [14875902808009077773, 11057756785998845984], [9284280664983346442, 2698603371801118240], [5436772051465153752, 10596594991106320442], [1636701738662927939, 9025801396188850035]],
              [[10529531450291716904, 12124227404340937243], [15832241050617736512, 15992959620340322598], [16288072546268871797, 1420679099509877763], [2696732323752550789, 5945245692836369605], [2301736450754911862, 13746402945446538819], [15806824447456330179, 6792639859902175828], [17726489991699025798, 8641718604718405938], [10713922989448551627, 2833433974149396297]],
              [[1868108414647542010, 12242809997195671841], [17051213445961779029, 3657103879526162105], [16863757291702272542, 15997460306374677945], [13603112470850609347, 15899086349349389961], [13549187769058974639, 12740466209530393611], [3221246573201908999, 2380797119909805006], [4104156408386033733, 7270816001120396753], [14201069325680956278, 12133351292143512286]]]
    currIndex = 0
    isPlayerOne = None
    gameboard = None

    # Three tables
    p1Table = {}
    p2Table = {}
    heurTable = {}

    WHITE = (255, 255, 255)
    BLACK = (0, 0, 0)
    GREEN = (48, 171, 0)
    RED = (255, 0, 0)

    SQUARESIZE = 50
    WIDTH = 8 * SQUARESIZE
    HEIGHT = 8 * SQUARESIZE

    size = (WIDTH, HEIGHT)
    RADIUS = int(SQUARESIZE/2 - 5)
    myfont = None

    def __init__(self):

        self.isPlayerOne = True
        self.gameboard = board.Board(self.zTable)




    def draw_board(self, screen):
        for c in range(8):
            for r in range(8):
                pygame.draw.rect(screen, self.GREEN, [c*self.SQUARESIZE, r*self.SQUARESIZE, self.SQUARESIZE-1, self.SQUARESIZE-1])

        for r in range(7, -1, -1):
            for c in range(7, -1, -1):
                if self.gameboard.board[c][r] == 1:
                    pygame.draw.circle(screen, self.WHITE, (int(r*self.SQUARESIZE+self.SQUARESIZE/2), self.HEIGHT-int(c*self.SQUARESIZE+self.SQUARESIZE/2)), self.RADIUS)
                elif self.gameboard.board[c][r] == 2:
                    pygame.draw.circle(screen, self.BLACK, (int(r * self.SQUARESIZE + self.SQUARESIZE / 2), self.HEIGHT - int(c * self.SQUARESIZE + self.SQUARESIZE / 2)), self.RADIUS)
        pygame.display.update()

    # This will train the bot
    #bot needs new startgame function for 2 bots.
    def trainBot(self, p1, p2):


        counter = 0
        while counter < 1000:
            self.readP1Table()
            self.readP2Table()
            self.readHeurTable()

            print("Game ", counter)
            g.botVsBot(p1, p2)
            print("Game Started")
            #self.writeHeurTable(self.heurTable)
            self.writeP1Table(self.p1Table)
            self.writeP2Table(self.p2Table)
            self.isPlayerOne = True
            g.gameboard = board.Board(self.zTable)
            counter = counter + 1



    # In this scenario, p1 is a "stupid" bot (makes random moves) and p2 is the ai bot
    def botVsBot(self, p1, p2):
        pygame.init()

        screen = pygame.display.set_mode(self.size)
        self.myfont = pygame.font.SysFont("monspace", 30)
        self.draw_board(screen)
        pygame.display.update()

        while self.gameboard.numMoves != 60:
            if self.gameboard.isTerminal() == 0:
                print("It's a tie! Neither player can move")
                break
            # if it's p1's turn
            if self.isPlayerOne:
                moves = self.gameboard.getMoves(True)
                if len(moves) == 0:
                    print("BROKE DOWN")
                    print(moves)
                    print(self.gameboard.hash())
                    break
                choice = random.randint(0, len(moves)-1)
                print("made move: ", moves[choice])
                p1.makeMove(moves[choice], self.gameboard)
                self.draw_board(screen)

                done = self.gameboard.isTerminal()
                if done != 3:
                    if done == 1:
                        self.isPlayerOne = True
                        print("Player 2 can't play")
                    elif done == 2:
                        self.isPlayerOne = False
                        print("Player 1 can't play")
                    elif done == 0:
                        print("Neither player can move. It's a tie!")
                        self.gameboard.numMoves = 60
                else:
                    self.isPlayerOne = not self.isPlayerOne
            else:
                print("Trying...")
                move = p2.findMove(self.gameboard)
                print("Found move: ", move)
                print("Finished")
                p2.makeMove(move, self.gameboard)
                self.draw_board(screen)
                # Determine whose turn is next
                done = self.gameboard.isTerminal()
                if done != 3:
                    if done == 1:
                        self.isPlayerOne = True
                        print("Player 2 can't play")
                    elif done == 2:
                        self.isPlayerOne = False
                        print("Player 1 can't play")
                    elif done == 0:
                        print("Neither can player can move. It's a tie!")
                        self.gameboard.numMoves = 60

                else:
                    self.isPlayerOne = not self.isPlayerOne

            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    print(self.gameboard.hash())
                    print(self.p2Table)
                    self.gameboard.numMoves = 60
                pygame.display.update()

        if self.gameboard.tileTally() > 0:
            label = self.myfont.render("Player 1 wins!", 1, g.RED)
        elif self.gameboard.tileTally() < 0:
            label = self.myfont.render("Player 2 wins!", 1, g.RED)
        else:
            label = self.myfont.render("It's a tie!", 1, g.RED)

        screen.blit(label, (40, 10))
        pygame.display.update()
        print(self.gameboard.hash())
        pygame.time.wait(3000)


    # Starts the GUI for Othello given an AI p2
    # startGame uses the pygame module to render the GUI components for Othello.
    # The game essentially loops between players until there is a tie or until the board is full
    # Everytime a player makes a move, draw_board is called to update the GUI
    # There are two event listeners included:
    # - A mouse event listener for the human player (click where you want to move)
    # - An exit event listener that will quit the game
    # Once the game has reached a terminal state (ie. a draw or the board is full), the total tally
    # for each player's tiles is calculated and a label is rendered onto the screen showing who won.
    def startGame(self, p2 = None):
        pygame.init()

        screen = pygame.display.set_mode(self.size)
        self.myfont = pygame.font.SysFont("monspace", 30)
        self.draw_board(screen)
        pygame.display.update()

        # While game is not over. Only have to check nummoves instead of is terminal to save time
        while self.gameboard.numMoves != 60:
            # If we are playing against an AI
            if p2 != None:
                # If it's the AI's turn
                if not self.isPlayerOne:
                    print("Trying...")
                    move = p2.findMove(self.gameboard)
                    print("Found move: ", move)
                    print("Finished")
                    p2.makeMove(move, self.gameboard)
                    self.draw_board(screen)
                    # Determine whose turn is next
                    done = self.gameboard.isTerminal()
                    if done != 3:
                        if done == 1:
                            self.isPlayerOne = True
                            print("Player 2 can't play")
                        elif done == 2:
                            self.isPlayerOne = False
                            print("Player 1 can't play")
                        elif done == 0:
                            print("Neither can player can move. It's a tie!")
                            self.gameboard.numMoves = 60

                    else:
                        self.isPlayerOne = not self.isPlayerOne

            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    print(self.gameboard.hash())
                    print(self.p2Table)
                    self.gameboard.numMoves = 60
                pygame.display.update()


                if event.type == pygame.MOUSEBUTTONDOWN:

                    posx = event.pos[0]
                    posy = event.pos[1]
                    col = int(math.floor(posx / self.SQUARESIZE))
                    row = 7 - int(math.floor(posy / self.SQUARESIZE))

                    # If the move is valid, then we change whose turn it is while making the move at the same time
                    if self.gameboard.makeMove((col, row), self.isPlayerOne) != None:
                        # Check if only one of the players can play
                        done = self.gameboard.isTerminal()
                        if done != 3:
                            if done == 1:
                                self.isPlayerOne = True
                                print("Player 2 can't play")
                            elif done == 2:
                                self.isPlayerOne = False
                                print("Player 1 can't play")
                            elif done == 0:
                                print("Neither can player can move. It's a tie!")
                                self.gameboard.numMoves = 60

                        else:
                            self.isPlayerOne = not self.isPlayerOne

                    self.draw_board(screen)

        if self.gameboard.tileTally() > 0:
            label = self.myfont.render("Player 1 wins!", 1, g.RED)
        elif self.gameboard.tileTally() < 0:
            label = self.myfont.render("Player 2 wins!", 1, g.RED)
        else:
            label = self.myfont.render("It's a tie!", 1, g.RED)

        screen.blit(label, (40, 10))
        pygame.display.update()
        print(self.gameboard.hash())
        pygame.time.wait(3000)






    def writeP1Table(self, dict):
        f = open("C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA\\StorageP1.txt", "a")

        pTable = {}
        my_int_list = [int(v) for v in open('C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA'
                                            '\\StorageP1.txt').read().split()]
        counter = 0
        print("readFile")
        while counter < len(my_int_list):
            pTable[my_int_list[counter]] = (my_int_list[counter + 1], my_int_list[counter + 2])
            counter = counter + 3
        for key, val in dict.items():
            try:
                pTable[key]
            except:
                f.write(str(key) + " " + str(val[0]) + " " + str(val[1]) + " ")

    def writeP2Table(self, dict):
        f = open("C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA\\StorageP2.txt", "a")

        pTable = {}
        my_int_list = [int(v) for v in
                       open('C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA'
                            '\\StorageP2.txt').read().split()]
        counter = 0
        while counter < len(my_int_list):
            pTable[my_int_list[counter]] = (my_int_list[counter + 1], my_int_list[counter + 2])
            counter = counter + 3
        for key, val in dict.items():
            try:
                pTable[key]
            except:
                f.write(str(key) + " " + str(val[0]) + " " + str(val[1]) + " ")

    def writeHeurTable(self, dict):
        f = open("C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA\\Storage.txt", "a")

        pTable = {}
        my_int_list = [int(v) for v in open('C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA'
                                            '\\Storage.txt').read().split()]
        counter = 0
        while counter < len(my_int_list):
            pTable[my_int_list[counter]] = my_int_list[counter + 1]
            counter = counter + 2
        for key, val in dict.items():
            try:
                pTable[key]
            except:
                f.write(str(key) + " " + str(val) + " ")

    def readP1Table(self):
        my_int_list = [int(v) for v in open('C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA'
                                            '\\StorageP1.txt').read().split()]
        counter = 0
        print("readFile")
        while counter < len(my_int_list):
            self.p2Table[my_int_list[counter]] = (my_int_list[counter + 1], my_int_list[counter + 2])
            counter = counter + 3

    def readP2Table(self):
        print("readFile")
        my_int_list = [int(v) for v in
                       open('C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA'
                            '\\StorageP2.txt').read().split()]
        counter = 0
        while counter < len(my_int_list):
            self.p2Table[my_int_list[counter]] = (my_int_list[counter + 1], my_int_list[counter + 2])
            counter = counter + 3

    def readHeurTable(self):
        pickle_in = open("C:\\Users\\stout\\Downloads\\Bot\\othellobot-master\\src\\DATA\\Storage.txt", "r")
        # self.heurTable = pickle.load(pickle_in)
        print("readFile")
        line = 3
        while True:
            try:
                line = pickle_in.readline()
                v = line.split(" ")
                self.heurTable[v[0]] = v[1]
                # print("RAN")
            except:
                break

if __name__ == "__main__":
    g = Game()
    p1 = player.Player(6, True, g)
    p2 = player.Player(6, False, g)
    g.trainBot(p1, p2)
    #g.startGame()
