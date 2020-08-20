import array



# (2, 6)
#Can't move there! Try again next time
#Player 1 can't play
# 17654764147978026283123462808848561696080552

# Terminates game too early:
# 18600075543338754568454765872423007095051600

#
# Can't move there (7,6)
# Player 1 can't play
# 19876139403893356013156372040190961271908522
# zHash: 102

# 16729208069399740786709287906972917246394538
# zHash: 46





class Board(object):
    HEIGHT = 8
    WIDTH = 8
    zTable = []
    lastPiecesFlipped = []
    p1tally = 0
    p2tally = 0



    def __init__(self, zTable, copy = None, hash = None):
        # If we need to copy a board
        if (copy):
            self.board = [list(columns) for columns in copy.board]
            self.lastMove = copy.lastMove
            self.zTable = zTable
            self.makeHash()
            self.numMoves = 0
            return

        # Deconstructs
        elif(hash) :
            self.board = []
            self.lastMove = None
            self.numMoves = 0
            self.zTable = zTable


            # convert to base 4 (4 because we have 3 options: 0 (empty space) 1 (white) 2(black) 3(end of col)
            digits = []
            while hash:
                digits.append(int(hash % 4))
                hash //= 4

            col = []

            for item in digits:

                # 3 indicates new column
                if item == 3:
                    self.board.append(col)
                    col = []

                # otherwise directly append base number
                else:
                    col.append(item)

            for i in range(8):
                for j in range(8):
                    spot = self.board[i][j]
                    if spot != 0:
                        # For building board from has, we need to update p1 tally and p2tally
                        if spot == 1:
                            self.p1tally += 1
                        elif spot == 2:
                            self.p2tally += 1
                        # Finally update numMoves by the tallies
            self.numMoves += self.p1tally + self.p2tally
            self.numMoves -= 4
            self.makeHash()
            return

        # Create a new board otherwise
        else:
            self.board = [[0 for x in range(self.WIDTH)] for y in range(self.HEIGHT)]
            self.numMoves = 0
            self.lastMove = None
            self.board[3][3] = 1
            self.board[4][3] = 2
            self.board[3][4] = 2
            self.board[4][4] = 1
            self.zTable = zTable
            self.makeHash()
            return


    # ZOBRIST HASHING
    # This section of code will contain all items necessary for using a zobrist hash:
    # The actual value of the hash zHash
    # The table containing the random 32 (should be 64 but idk how to make that) bit numbers to represent each piece at each position
    # It is important to note that these random numbers do not correspond to a piece's suit alone, but rather
    # its suit as well as the specific position on the board
    # So our table of random numbers will be 8 x 8 x 2 (64 positions with 2 random numbers representing each suit)
    # WHITE will be indexed as 0 and BLACK will be indexed as 1 in these subarrays
    # Note that zTable, genNum, and initTable should all be contained in the main game class since these numbers should be the same across all board states
    # This doesn't matter too much since the values will be the same even if generated for each board, but it only needs to be done once for all boards
    zHash = 0

    # Makes a zHash by looking at the whole board
    def makeHash(self):
        self.zHash = 0
        for row in range(8):
            for col in range(8):
                spot = self.board[row][col]
                if spot != 0:

                    # Assign the proper index for the zobrist table of the piece

                    piece = 0 if spot == 1 else 1

                    self.zHash ^= self.zTable[row][col][piece]

    # Updates the zHash by XORing out the current pieces to be flipped and XORing in the new pieces
    def updateHash(self, move, positions, isPlayerOne):
        # Note that these are indices for the zTable. Not the board values
        currPlayer = 0 if isPlayerOne else 1
        oppPlayer = 1 if isPlayerOne else 0
        # First we XOR in the actual move played
        self.zHash ^= self.zTable[move[1]][move[0]][currPlayer]
        for pos in positions:
            self.zHash ^= self.zTable[pos[0]][pos[1]][oppPlayer]
            self.zHash ^= self.zTable[pos[0]][pos[1]][currPlayer]

    # Updates the zHash using the same process as updateHash but it is called when undoing a move
    def undoHash(self, move, positions, isPlayerOne):
        currPlayer = 1 if isPlayerOne else 0 # Notice these values are flipped compared to above. Since after makemove, the isPlayerOne status if flipped. So we must keep in mind the player that played previously
        oppPlayer = 0 if isPlayerOne else 1
        # XOR out the move that was made
        self.zHash ^= self.zTable[move[1]][move[0]][currPlayer]
        # Replace all tiles that were changed with their former values
        for pos in positions:
            self.zHash ^= self.zTable[pos[0]][pos[1]][currPlayer]
            self.zHash ^= self.zTable[pos[0]][pos[1]][oppPlayer]




    # Hash function. This function uses a similar has function as the connect 4 problem
    # since both problems use a two dimensional array two keep track of pieces
    # Ours is slightly different since our array is initialized to full capacity at start
    # For this reason, we have to use a power of 4 since we will have 3 cases to examine
    def hash(self):
        power = 0
        hash = 0

        for column in self.board:

            # add 1 or 2 depending on piece
            for piece in column:
                hash += piece * (4 ** power)
                power += 1

            # add a 3 to indicate end of column
            hash += 3 * (4 ** power)
            power += 1

        return hash



    # Print function that prints the board
    # last row array in the boards array is the top row printed
    # prints columns from left to right
    # Returns the number of tiles taken
    def boardprint(self):
        print("")
        print("+" + "---+" * self.WIDTH)
        for rowNum in range(self.HEIGHT - 1, -1, -1):
            row = "|"
            for colNum in range(self.WIDTH):
                val = ""
                if self.board[rowNum][colNum] == 1:
                    val = "W"
                elif self.board[rowNum][colNum] == 2:
                    val = "B"
                else:
                    val = " "
                row += " " + val + " " + "|"
            print(row)
            print ("+" + "---+" * self.WIDTH)


    # Undoes the last move made
    def undoMove(self, move, flips, isPlayerOne):
        # Make the last move an empty spot
        self.board[move[1]][move[0]] = 0
        lastTile = 1 if isPlayerOne else 2 # If it's player one's turn at this moment, then the last move was made by player 2. So all tiles captured in flips were tiles of player 1
        for pos in flips:
            self.board[pos[0]][pos[1]] = lastTile
        self.undoHash(move, flips, isPlayerOne)
        self.numMoves -= 1
        if isPlayerOne:
            self.p2tally -= len(flips) + 1
        else:
            self.p1tally -= len(flips) + 1

    # Returns none if not na valid move and the pos if valid
    def makeMove(self, pos, isPlayerOne):
        if not self.isValidMove(pos, isPlayerOne):
            print("Can't move there! Try again next time")
            return None

        # If the program hasn't returned, then there are capturable tiles
        # So we must check every direction using a similar algorithm as isValidMove()
        currPlayer = 1 if isPlayerOne else 2
        oppPlayer = 2 if isPlayerOne else 1

        capturable = False

        # Pieces to flip array
        # This array will contain the indices for the tiles to be flipped at the end of the loops
        pieces = []

        #Horizontal right check
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        for col in range(pos[0] + 1, self.WIDTH):

            currTile = self.board[pos[1]][col]

            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces = pieces + temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break


            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True
                temp.append([pos[1], col])

        # Horizontal Left
        capturable = False
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        for col in range(pos[0] - 1, -1, -1):
            currTile = self.board[pos[1]][col]

            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces = pieces + temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True
                temp.append([pos[1], col])

        # Vertical down
        capturable = False
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        for row in range(pos[1] - 1, -1, -1):
            currTile = self.board[row][pos[0]]

            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces = pieces + temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True
                temp.append([row, pos[0]])

        # Vertical up
        capturable = False
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        for row in range(pos[1] + 1, self.HEIGHT):
            currTile = self.board[row][pos[0]]

            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces = pieces + temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True
                temp += [[row, pos[0]]]


        # Diagonal up right
        capturable = False
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        # Have to keep track of two numbers (x, y)
        x = pos[0] + 1
        y = pos[1] + 1
        while x < self.WIDTH and y < self.HEIGHT:
            currTile = self.board[y][x]

            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces += temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:


                capturable = True
                temp += [[y, x]]

            # increment x and y by 1
            x += 1
            y += 1

        # Diagonal up left
        capturable = False
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        # Have to keep track of two numbers (x, y)
        x = pos[0] - 1
        y = pos[1] + 1
        while x >= 0 and y < self.HEIGHT:
            currTile = self.board[y][x]


            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces = pieces + temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True
                temp += [[y, x]]

            x -= 1
            y += 1

        # Diagonal down right
        capturable = False
        # Have to keep track of two numbers (x, y)
        x = pos[0] + 1
        y = pos[1] - 1
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        while x < self.WIDTH and y >= 0:
            currTile = self.board[y][x]


            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces = pieces + temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True
                temp += [[y,x]]

            x += 1
            y -= 1

        # Diagonal down left
        capturable = False
        # Have to keep track of two numbers (x, y)
        x = pos[0] - 1
        y = pos[1] - 1
        # a temporary array to hold candidate flips. Only add to final when we encounter a piece of the same suit
        temp = []
        while x >= 0 and y >= 0:
            currTile = self.board[y][x]


            # If we encounter a blank spot, then nothing can be captured in this row (since it breaks the chain of tiles)
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                pieces = pieces + temp
                break

            # If capturable is not true but we see a tile with the same suit as our player, then we break
            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True
                temp += [[y, x]]

            x -= 1
            y -= 1


        # Now that we have seen all directions and recorded all instances of capturable spots, we will iterate over the positions
        # and change them to our current player. (temp array stores it in (y, x) form)
        self.board[pos[1]][pos[0]] = 1 if isPlayerOne else 2
        for index in pieces:
            self.board[index[0]][index[1]] = currPlayer
        self.numMoves += 1
        self.updateHash(pos, pieces, isPlayerOne)
        self.lastPiecesFlipped = pieces
        self.lastMove = pos
        if isPlayerOne:
            self.p1tally += 1 + len(pieces)
        else:
            self.p2tally += 1 + len(pieces)
        return pos






    # boolean
    # NOTE pos should come in as (x, y). ie x should represent the column and y should represent the row. So (1, 0) is the
    # second position in the first array (row)
    def isValidMove(self, pos, isPlayerOne):
        # Representations for player1 and player2 are represrented as ints 1 and 2
        currPlayer = 1 if isPlayerOne else 2
        oppPlayer = 2 if isPlayerOne else 1
        # When we make a move, we must be capturing another piece
        # So this function will have to check that there is a capturable piece
        # as well as another piece of the same suit that can induce a capture of found opponent pieces
        # If the piece immediately next to us in a specified direction (ex. horizontal) is of the same suit,
        # then we cannot place a piece with the intent of capturing an opponent in that direction (since the tile next to it
        # will be closing off its capture range)

        # First case is making sure there is no other piece in this spot
        if self.board[pos[1]][pos[0]] != 0:
            return False

        # Start by checking the horizontal right pieces.
        capturable = False
        for col in range(pos[0] + 1, self.WIDTH):
            currTile = self.board[pos[1]][col]
            # Col starts out at the spot to the right of the tile
            # For ever iteration, the tile we are looking at will shift to the right by 1
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made
            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break
            # Alternatively, if we encounter a tile of the same suit and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True

            # If our currTile is equal to the currPlayer and capturable is not true, then there is nothing to capture. So break
            if (currTile == currPlayer):
                break


            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True

        # Now we check horizontal left if we haven't found a valid move yet
        capturable = False
        for col in range(pos[0] - 1, -1, -1):
            currTile = self.board[pos[1]][col]
            # Col starts out at the spot to the left of the tile
            # For ever iteration, the tile we are looking at will shift to the left by 1
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made

            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break
            # Alternatively, if we encounter a suit tile and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True

            if (currTile == currPlayer):
                break


            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True

        # Now we do vertical down if we haven't found a move yet
        capturable = False
        for row in range(pos[1] - 1, -1, -1):
            currTile = self.board[row][pos[0]]
            # Col starts out at the spot to the left of the tile
            # For ever iteration, the tile we are looking at will shift up by 1
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made

            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break
            # Alternatively, if we encounter a suit space and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True


            if (currTile == currPlayer):
                break


            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True

        # Vertical up
        capturable = False
        for row in range(pos[1] + 1, self.HEIGHT):
            currTile = self.board[row][pos[0]]
            # Col starts out at the spot to the left of the tile
            # For ever iteration, the tile we are looking at will go down by 1
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made

            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break
            # Alternatively, if we encounter a suit space and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True

            if (currTile == currPlayer):
                break


            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True

        # Diagonal up right
        capturable = False
        # Have to keep track of two numbers (x, y)
        x = pos[0] + 1
        y = pos[1] + 1
        while x < self.WIDTH and y < self.HEIGHT:
            currTile = self.board[y][x]
            # Col starts out at the spot to the left of the tile
            # For ever iteration, the tile we are looking at is the upper right tile
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made

            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break


            # Alternatively, if we encounter a blank space and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True

            if (currTile == currPlayer):
                break


            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True


            # increment x and y by 1
            x += 1
            y += 1

        # Diagonal up left
        capturable = False
        # Have to keep track of two numbers (x, y)
        x = pos[0] - 1
        y = pos[1] + 1
        while x >= 0 and y < self.HEIGHT:
            currTile = self.board[y][x]
            # Col starts out at the spot to the left of the tile
            # For ever iteration, the tile we are looking at is the upper right tile
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made

            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break

                # Alternatively, if we encounter a blank space and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True

            if currTile == currPlayer:
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True

            # decrement x and increment y by 1
            x -= 1
            y += 1

        # Diagonal down right
        capturable = False
        # Have to keep track of two numbers (x, y)
        x = pos[0] + 1
        y = pos[1] - 1
        while x < self.WIDTH and y >= 0:
            currTile = self.board[y][x]
            # Col starts out at the spot to the left of the tile
            # For ever iteration, the tile we are looking at is the upper right tile
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made

            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break

                # Alternatively, if we encounter a blank space and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True

            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True

            # increment x and decrement y by 1
            x += 1
            y -= 1

        # Diagonal down left
        capturable = False
        # Have to keep track of two numbers (x, y)
        x = pos[0] - 1
        y = pos[1] - 1
        while x >= 0 and y >= 0:
            currTile = self.board[y][x]
            # Col starts out at the spot to the left of the tile
            # For ever iteration, the tile we are looking at is the upper right tile
            # So if we encounter a tile of the same suit before we have found a move (ie. returned True), then we will break the loop
            # Since no move can be made

            # If we encounter a blank space before making a move, break
            if currTile == 0:
                break

                # Alternatively, if we encounter a current space and we are able to capture tiles, we return true
            if capturable and currTile == currPlayer:
                return True

            if (currTile == currPlayer):
                break

            # If we encounter an opposing player's piece, then we can mark this as capturable (potentially)
            if currTile == oppPlayer:
                capturable = True

            # increment x and y by 1
            x -= 1
            y -= 1

        # If nothing has been returned by this point, return false
        return False


    # returns 0 if neither can play
    # returns 1 if player 1 can play
    # returns 2 if player 2 can play
    # returns 3 if both can play
    def isTerminal(self):
        if (self.numMoves == 60):
            return 0
        total = 3
        if(self.isTerminalOne()):
            total -= 1
        if(self.isTerminalTwo()):
            total -= 2
        return total

    # boolean
    def isTerminalOne(self):
        if len(self.getMoves(True)) == 0:
            return True
        else:
            return False
    def isTerminalTwo(self):
        if len(self.getMoves(False)) == 0:
            return True
        else:
            return False



    # Difference between counts of white and black
    # Positive if white (player 1) has more
    # 0 if tie
    # Negative black has more
    # Prints the counts of each
    def tileTally(self):

        return self.p1tally - self.p2tally

    # Generates a list of tuples of the form [[move], board]
    def children(self, isPlayerOne):
        children = []
        moves = self.getMoves(isPlayerOne)
        for move in moves:
            child = Board(self.zTable, self)
            child.makeMove(move, isPlayerOne)
            children.append((move, child))
        return children

    
    def getMoves(self, isPlayerOne):
        #Moves will hold lists of valid move POSITIONS "(x,y)" for p1 and p2
        potentialMoves = []

        #iterate through board and find potential moves
        for row in range(self.HEIGHT):
            for col in range(self.WIDTH):
                pos = (row, col)

                if (self.isValidMove(pos, isPlayerOne)):
                    potentialMoves.append(pos)
        return potentialMoves
