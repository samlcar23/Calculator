using System;

//Using String.split
//https://docs.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split

/*****************************************************************
*Command line Calculator that allows one line input.
*
*@author Sam Carson
*@version Winter 2018
*****************************************************************/
public class Calculator{

	/** Keeps track of running total of calculations */
	public static double runningTotal = 0;

	/** Keeps track of the last result */
	public static double result = 0;

	/*****************************************************************
	*Method to perform the actual calculation.
	*
	*@param parts the individual parts of the calcualtion in an array
	******************************************************************/
	static private double Calculate(string[] parts){
		double temp;
		double answer = 0;

		//Array starts with a number
		if(Double.TryParse(parts[0], out temp)){

			double a;
			double b;

			//First operand
			Double.TryParse(parts[0], out a);

			//Second operand
			Double.TryParse(parts[2], out b);

			//checks the operator
			switch (parts[1]){
				case "+":
					answer = a + b;
					return answer;
				case "-":
					answer = a - b;
					return answer;
				case "*":
					answer = a * b;
					return answer;
				case "/":
					if(b == 0){
						Console.WriteLine("You can't divide by 0");
						return double.NaN;
					}
					answer = a / b;
					return answer;
				case "^":
					answer = Math.Pow(a, b);
					return answer;
			}
			return answer;

		//Array starts with an operand
		}else{

			double a;

			//the only operand
			Double.TryParse(parts[1], out a);

			//checks the operator
			switch (parts[0]){
				case "+":
					answer = result + a;
					return answer;
				case "-":
					answer = result - a;
					return answer;
				case "*":
					answer = result * a;
					return answer;
				case "/":
					if(a == 0){
						Console.WriteLine("You can't divide by 0");
						return double.NaN;
					}
					answer = result / a;
					return answer;
				case "^":
					answer = Math.Pow(result, a);
					return answer;
			}
			return answer;
		}
	}

	/************************************************************
	*Method to make sure operator is a valid character.
	*
	*@param part the char to be checked
	*************************************************************/
	static private bool checkChar(string part){
		switch (part){
				case "+":
					return true;
				case "-":
					return true;
				case "*":
					return true;
				case "/":
					return true;
				case "^":
					return true;
				default:
					return false;
			}
	}

	/*****************************************************************
	*Method to make sure the input is a valid form.
	*
	*@param parts the individual parts of the calcualtion in an array
	******************************************************************/
	static private bool ValidInput(string[] parts){
		double temp = 0;
		
		//longer than 3 arguments
		if(parts.Length > 3){
			return false;
		}
		//expression starting with operand ie: + 2
		if(parts.Length == 2){
			if(!Double.TryParse(parts[0], out temp) && checkChar(parts[0])){
				if(Double.TryParse(parts[1], out temp)){
					return true;
				}
			}
		}
		//normal expression ie: 2 + 2
		if(parts.Length == 3){
			if (Double.TryParse(parts[0], out temp)){
				if(!Double.TryParse(parts[1], out temp) && checkChar(parts[1])){
					if(Double.TryParse(parts[2], out temp)){
						return true;
					}
				}
			}
		}
		return false;
	}

	/*****************************************************************************************
	*Main method to run the Calculator program and print out all useful information
	*for the user to reference.
	*****************************************************************************************/
	static public void Main(){
		//prints the welcome message and help to the console
		Console.WriteLine("\nWelcome to Sam's calculator!\n");
		Console.WriteLine("For directions type 'help'");
		Console.WriteLine("To quit type 'quit'\n");
		Console.WriteLine("\nType an expression seperated by spaces and then press enter");
		Console.WriteLine("Examples: 2 + 2 or + 2\n");


		bool running = true;

		while(running){
		
			string operation = Console.ReadLine();
			string[] parts = operation.Split(' ');

			//Quits the program
			if(operation.ToLower().Equals("quit") || operation.ToLower().Equals("exit")){
				running = false;
				break;
			}

			//sets running total and result to 0
			if(operation.ToLower().Equals("clear")){
				runningTotal = 0;
				result = 0;
				Console.WriteLine("\nRunning Total = " + runningTotal);
				Console.WriteLine("\nResult = " + result);
				continue;
			}

			//Help screen
			if(operation.ToLower().Equals("help")){
				Console.WriteLine("You can enter an expression that is three arguments in length seperated by spaces.");
				Console.WriteLine("Examples include:\n2 + 2\n35 * 6\n10 / 2\n2 ^ 3\n");
				Console.WriteLine("You can also use the last result as the first argument by starting your expression with the operand.");
				Console.WriteLine("Examples include:\n+ 2\n* 10\n/ 3\n^ 2\n");
				Console.WriteLine("Commands:\n'quit' Exits the calculator\n'clear' Clears last result and running total\n'help' Shows directions");
				continue;
			}

			//Check for valid input and do calculation if it is valid
			if(ValidInput(parts) == true){
				result = Calculate(parts);
			} else {
				Console.WriteLine("Invalid input");
			}

			//Print running total and answer if result isn't double.NaN
			if (!double.IsNaN(result)) {
				runningTotal += result;
				Console.WriteLine("\nRunning Total = " + runningTotal);
				Console.WriteLine("\nResult = " + result);
			}
		}
	}
}