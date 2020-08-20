import board
import array

class Player(object):
    isPlayerOne = None
    score = 0
    WEIGHTS = [[99, -8, 8, 6, 6, 8, -8, 99],
               [-8, -24, -4, -3, -3, -4, -24, -8],
               [8, -4, 7, 0, 0, 7, -4, 8],
               [6, -3, 0, 4, 4, 0, -3, 6],
               [6, -3, 0, 4, 4, 0, -3, 6],
               [8, -4, 7, 0, 0, 7, -4, 8],
               [-8, -24, -4, -3, -3, -4, -24, -8],
               [99, -8, 8, 6, 6, 8, -8, 99]]
    WEIGHTS2 = [[198, 8, 16, 12, 12, 16, 8, 198],
               [8, 24, 4, 3, 3, 4, 24, 8],
               [16, 4, 14, 0, 0, 14, 4, 16],
               [12, 3, 0, 8, 8, 0, 3, 12],
               [12, 3, 0, 8, 8, 0, 3, 12],
               [16, 4, 14, 0, 0, 14, 4, 16],
               [8, 24, 4, 3, 3, 4, 24, 8],
               [198, 8, 16, 12, 12, 16, 8, 198]]
    CORNERS = [(0,0),(0,7),(7,0),(7,7)]
    depthLimit = 0

    # game will be used to reference to the global game class. Used for reading and writing
    # to tables
    game = None



    def __init__(self, depthLimit, isPlayerOne, game):
        self.isPlayerOne = isPlayerOne
        self.depthLimit = depthLimit
        self.game = game



    def makeMove(self, pos, board):
        board.makeMove(pos, self.isPlayerOne)


    # Undoes the last move made on the board
    def undoMove(self, board):
        board.undoMove(board.lastMove, board.lastPiecesFlipped, self.isPlayerOne)
    
    
    def heuristic(self, board):
        heuristic = 3 * (len(board.getMoves(True)) - len(board.getMoves(False)))
        
        #goes through process of checking for stable corners for all four corners
        #at each corner, iterates through vertical and horizontal from corner, checking if values are the same
        #if entire vertical or horizontal isn't equal to corner, records vertical and horizontal index where 
        #this occurs. It will then move diagonally in one cell (from 0,0 to 1,1 for example) then checks
        #vertical and horizontal from that corner. stops one before failure indexes from before, because inner row/column
        #will need to be 1 value shorter than previous outer row/col in order to be protected from diagonals. THis does not apply 
        #when all 8 cells are equal to corner(no way to take from diagonal)
        for IthCorner in self.CORNERS:
            #checks value of tile at each corner(0,1,or 2)
            corner = board.board[IthCorner[0]][IthCorner[1]]
            # if it isn't 0
            if(corner != 0):
                #vertsteps checks whether to move left or right(1 or -1) from corner during iteration
                vertSteps = 1
                horiSteps = 1
                #whenever corner has y value or x value of 7, will have to move right or down(step will need to be -1)
                if(IthCorner[0] == 7):
                    vertSteps = -1
                if(IthCorner[1] == 7):
                    horiSteps = -1
                #vertFail and horiFail check what vertical or horizontal index value fails to be equal to corner value
                vertFail = 9
                horiFail = 9
                #need to assign iteration range to variable prior to starting so we can dynamically alter range with each iteration
                r = [0,1,2,3,4,5,6,7]
                #i is the minimum of horizontal or vertical failure
                for i in r:
                    for v in range(i, vertFail - 1):
                        #vertical space is corner index, plus step direction value times v (will give vertical index)
                        space = IthCorner[0] + vertSteps * v
                        #same thing but for horizontal index
                        otherspace = IthCorner[1] + horiSteps * i
                        if(board.board[space][otherspace] != corner):
                            vertFail = v
                            break
                        else:
                            self.WEIGHTS[space][otherspace] = self.WEIGHTS2[space][otherspace]
                    #do same stuff for horizontal
                    for h in range(i, horiFail - 1):
                        space = IthCorner[1] + horiSteps * h
                        otherspace = IthCorner[0] + vertSteps * i
                        if(board.board[otherspace][space] != corner):
                            horiFail = h
                            break
                        else:
                            self.WEIGHTS[otherspace][space] = self.WEIGHTS2[otherspace][space]
                    #only check rows and cols up to min of either previous vertfail or horifail
                    if(len(r) >= min(vertFail,horiFail) + 1):
                        del r[min(vertFail, horiFail):]
                        
        #calculate heuristic based on weights
        for row in range(board.HEIGHT):
            for col in range(board.WIDTH):
                if board.board[row][col] != 0:
                    heuristic += (self.WEIGHTS[row][col] * (-1 if board.board[row][col] == 2 else 1)) # round(2 * (1.5 - board.board[row][col])))

        return heuristic


    # Helper function to initiate alpha beta pruning. First checks if there is a first move already stored in the table
    # for the respective player
    # Returns a tuple of the form [heuristic, [x, y]]
    def findMove(self, board):
        alpha = float('-inf')
        beta = float('inf')

        # Check if a move is readily available for the current board
        if self.isPlayerOne:
            if board.zHash in self.game.p1Table:
                print("found move in p1Table")
                return self.game.p1Table[board.zHash]
        else:
            if board.zHash in self.game.p2Table:
                print("Found move in p2Table")
                return self.game.p2Table[board.zHash]

        bestMove = self.alphaBeta(board, self.depthLimit, [alpha, [0,0]], [beta, [0,0]], self.isPlayerOne, self.game.heurTable)[1]
        if board.isValidMove(bestMove, self.isPlayerOne):
            if self.isPlayerOne:
                self.game.p1Table[board.zHash] = bestMove
            else:

                self.game.p2Table[board.zHash] = bestMove

        return bestMove




    # alphabeta pruning
    # Writes to heurTable whenever a new board is encountered
    def alphaBeta(self, board, depth, alpha, beta, isMax, heurTable):
        terminated = board.isTerminal()
        if terminated == 0:
                return [1000000000 if isMax else -100000000, [-1,-1]]

        if terminated == 1:
            isMax = True

        if terminated == 2:
            isMax = False

        if depth == 0:
            # If the zHash is in the heurTable, we know its heuristic already
            if board.zHash in heurTable:
                return [heurTable[board.zHash], [-1,-1]]
            else:
                print("Trying to get heuristic...")
                heur = self.heuristic(board)
                print("Finished heuristic")
                return [heur, [-1, -1]]

        for child in board.children(isMax):
            # Write to heurTable
            # Note that this is pseudocode as I am not certain of what the heurTable will be
            # looking like for sure
            if child[1].zHash not in heurTable:
                heurTable[child[1].zHash] = self.heuristic(child[1])
            # end write to heurTable

            move = self.alphaBeta(child[1], depth - 1, alpha, beta, not isMax, heurTable)
            if alpha[0] >= beta[0]:
                break
            if isMax:
                if move[0] > alpha[0]:
                    alpha = move[0], child[0]

            else:
                if move[0] < beta[0]:
                    beta = move[0], child[0]


        if isMax:
            return alpha
        else:
            return beta
    
