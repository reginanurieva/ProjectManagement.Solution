//Drag and Drop functionality
function allowDrop(event) {
  event.preventDefault();
}

//Sets the data in the draggable object to be its id
function drag(event) {
  event.dataTransfer.setData("text", event.target.id)
}

function drop(event, element) {
  event.preventDefault();
  //Gets the todo Id from the draggable card, and column name from column where it was dropped
  var todoId = event.dataTransfer.getData("text");
  var columnName = $(element).prop('id');
  //Adds the card to the new element
  element.appendChild(document.getElementById(todoId));
  //Adds the card id as the value to hiddent input "todoId"
  $('#' + columnName + '-input').attr('value', todoId);
  //Submits the form that the column is nested inside.
  $(element).parent('form').submit();
}

$(document).ready(function() {
});
