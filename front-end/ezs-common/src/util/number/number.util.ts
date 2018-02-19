class Util {

    public toDecimalCaseBefore = (input: number, decimalCases: number) => {
        let numberToCompare = input.toString().split('.')[0];
        let numberToReturn = input.toString();
        if (numberToCompare.length < decimalCases) {
            while (numberToCompare.length <= decimalCases) {
                numberToReturn = '0' + input;
                decimalCases--;
            }
        }
        return numberToReturn;
    }

}

export const NumberUtil = new Util();