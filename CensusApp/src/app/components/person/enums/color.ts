export enum Color {
    Black = 0,
    White = 1,
    Brown = 2,
    Indigenous = 3,
    Yellow = 4,
  }

  export function colorToString(color: Color): string {
    switch (color) {
      case Color.Black:
        return 'Black';
      case Color.White:
        return 'White';
      case Color.Brown:
        return 'Brown';
      case Color.Indigenous:
        return 'Indigenous';
      case Color.Yellow:
        return 'Yellow';
      default:
        return '';
    }
  }