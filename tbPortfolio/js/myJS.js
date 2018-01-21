
$(function(){

    /* Display JS Exercises */

      $('#displayStatsBtn').click(function(){
          $('#numStatsRow').show().fadeIn();
          $('#factorialRow').hide().fadeOut();
          $('#fizzRow').hide().fadeOut();
          $('#palindromeRow').hide().fadeOut();
      });

        $('#displayFactorialBtn').click(function(){
          $('#numStatsRow').hide().fadeOut();
          $('#factorialRow').show().fadeIn();
          $('#fizzRow').hide().fadeOut();
          $('#palindromeRow').hide().fadeOut();
      });

      $('#displayFizzBtn').click(function(){
          $('#numStatsRow').hide().fadeOut();
          $('#factorialRow').hide().fadeOut();
          $('#fizzRow').show().fadeIn();
          $('#palindromeRow').hide().fadeOut();
      });

      $('#displayPalindromeBtn').click(function(){
          $('#numStatsRow').hide().fadeOut();
          $('#factorialRow').hide().fadeOut();
          $('#fizzRow').hide().fadeOut();
          $('#palindromeRow').show().fadeIn();
      });

    /* BEGIN - NUMBER STATS GAME */
    $("#numGameSubmit").click(function(){
        var num1 = parseFloat($("#numInput1").val());
        var num2 = parseFloat($("#numInput2").val());
        var num3 = parseFloat($("#numInput3").val());
        var num4 = parseFloat($("#numInput4").val());
        var num5 = parseFloat($("#numInput5").val());

        var minNum = Number(Math.min(num1,num2, num3, num4, num5));
        var maxNum = Number(Math.max(num1,num2, num3, num4, num5));
        var sumNum = Number(num1 + num2 + num3 + num4 + num5);
        var meanNum = Number(sumNum / 5);
        var prodNum = Number(num1 * num2 * num3 * num4 * num5);

        // Statistics Output
        $("#numsOutput").append("<p>Lowest: " + minNum + "</p>");
        $("#numsOutput").append("<p>Greatest: " + maxNum + "</p>");
        $("#numsOutput").append("<p>Mean: " + meanNum + "</p>");
        $("#numsOutput").append("<p>Sum: " + sumNum+ "</p>");
        $("#numsOutput").append("<p>Product: " + prodNum + "</p>");
    });

    // Reset Number Statistics
    $("#numGameReset").click(function(){
        $("#numInput1").val("");
        $("#numInput2").val("");
        $("#numInput3").val("");
        $("#numInput4").val("");
        $("#numInput5").val("");
        $("#numsOutput").html("");
   });
   /* END - NUMBER STATS */


   /* BEGIN - FACTORIAL */
    $("#factorialSubmit").click(function() {
        var factInput = parseFloat($("#factorialInput").val());
        var answer = factInput;
        if (isNaN(factInput) || factInput % 1 !==0 || factInput <=0) {
            alert("Please enter a valid number!");
            return;
        } else {
            for (i = factInput - 1; i > 1; i--){
                answer *= i;
            }
        }

        // Output
        $("#factorialOutput").append("<p>The factorial of " + factInput + " = " + answer + "!</p>");
    });

    // Factorial Reset
    $("#factorialReset").click(function(){
        $("#factorialInput").val("");
        $("#factorialOutput").html("");

    });
    /* END - FACTORIAL */

    /* BEGIN - FIZZ BUZZ */
    $('#fizzBuzzSubmit').click(function(){
        var fizzNum = parseInt($('#fizzInput').val());
        var buzzNum = parseInt($('#buzzInput').val());

        if (isNaN(fizzNum) || fizzNum < 1
                           || fizzNum > 100
                           || isNaN(buzzNum)
                           || buzzNum < 1
                           || buzzNum > 100){

            alert("Please enter valid numbers between 1 and 100");
            return;
        } else {
            for (var i = 1; i <= 100; i++){
                if (i % fizzNum === 0 && i % buzzNum === 0 ){
                    printNum="FizzBuzz";
                } else if (i % fizzNum === 0){
                    printNum="Fizz";
                } else if (i % buzzNum === 0){
                    printNum="Buzz";
                } else {
                    printNum=i;
                }
                $('#fizzBuzzOutput').append(printNum + ", ");
            }
        }
        $('#fizzBuzzOutput').append("<br><br>");
    });

    $('#fizzBuzzReset').click(function(){
        $('#fizzInput').val("");
        $('#buzzInput').val("");
        $('#fizzBuzzOutput').html("");;
    });
    /* END - FIZZ BUZZ */


    /* BEGIN - PALINDROME */
    $('#palindromeSubmit').click(function(){
        var palindromePhrase = $('#palindromeInput').val();
        var reversePhrase = "";

        // Validate submission != empty string
        if (palindromePhrase.length < 1) {
            alert("Please enter a valid phrase!");
            return;
        }

        reversePhrase = palindromePhrase.split("").reverse().join("");
        if (palindromePhrase.length == 1 || reversePhrase == palindromePhrase){
            $('#palindromeOutput').append("<p> The word '" + palindromePhrase + "'' is a palindrome.</p>");
        } else {
            $('#palindromeOutput').append("<p> The word '" + palindromePhrase + "'' is not a palindrome.</p>");
        }
    });

    $('#palindromeReset').click(function(){
        $('#palindromeInput').val("");
        $('#palindromeOutput').html("");

    });
    /* END - PALINDROME */

});







