// Grapf definition
type Vortex<'a> = 'a

type Edge<'a> = Vortex<'a> * Vortex<'a>

type Graph<'a> = Vortex<'a> list * Edge<'a> list

// Tree definition
type Tree<'a> =
    | Branch of 'a * Tree<'a> list
    | Leaf of 'a

// DFA definition
type State<'a> = 'a

type States<'a> = State<'a> list

type Input<'b> = 'b

type Inputs<'b> = Input<'b> list

type InitialState<'a> = State<'a>

type TerminalStates<'a> = State<'a> list

type StateTransitions<'a, 'b> = (State<'a> -> Input<'b> -> State<'a>) list

type DFA<'a, 'b> = States<'a> * Inputs<'b> * StateTransitions<'a, 'b> * InitialState<'a> * TerminalStates<'a>

// NFA definition
type StateToStatesTransitions<'a, 'b> = (State<'a> -> Input<'b> -> States<'a>) list

type NFA<'a, 'b> = States<'a> * Inputs<'b> * StateToStatesTransitions<'a, 'b> * InitialState<'a> * TerminalStates<'b>

// PDA definition
type MemoryCell<'c> = 'c

type Memory<'c> = MemoryCell<'c> list

type InitialMemoryCell<'c> = MemoryCell<'c>

type PDATransitions<'a, 'b, 'c> = (State<'a> -> Input<'b> -> MemoryCell<'c> -> (State<'a> * MemoryCell<'c>) list) list

type PDA<'a, 'b, 'c> = States<'a> * Input<'b> * Memory<'c> * PDATransitions<'a, 'b, 'c> * InitialState<'a> * InitialMemoryCell<'c> * TerminalStates<'a>