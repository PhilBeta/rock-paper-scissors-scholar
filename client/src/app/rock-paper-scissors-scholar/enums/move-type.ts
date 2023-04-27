export enum MoveType {
    Rock,
    Paper,
    Scissors
}

export function parseMoveType(text:string): MoveType {
    if (text === undefined)
        throw new Error("Input is undefined.");

    let textUpper = text.toUpperCase();
    switch (textUpper){
        case "ROCK":
            return MoveType.Rock;
        case "PAPER":
            return MoveType.Paper;
        case "SCISSORS":
            return MoveType.Scissors;
        default:
            throw new Error("Error parsing move type.");
    }
}