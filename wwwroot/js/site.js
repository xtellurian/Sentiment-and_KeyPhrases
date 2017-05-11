﻿// Write your Javascript code.
 // $(document).ready
 function colorRows(){

  var mc = {
    '0-0.33'     : 'red-sentiment',
    '0.33-0.66'    : 'yellow-sentiment',
    '0.66-1.00'   : 'green-sentiment'
  };
  
function between(x, min, max) {
  return x >= min && x <= max;
}
  
  var dc;
  var first; 
  var second;
  var th;
  
  $('tr').each(function(index){
    
    th = $(this);
    
    dc = parseFloat($(this).attr('data-color'));
    
    
      $.each(mc, function(name, value){

        first = parseFloat(name.split('-')[0]);
        second = parseFloat(name.split('-')[1]);
        
        // console.log(between(dc, first, second));
        
        if( between(dc, first, second) ){
          th.addClass(value);
        }

    
    
      });
    
  });
}

function topicsTable(){
  var table = $('#topics').DataTable({
    
  });
 
  table
      .column( '0:visible' )
      .order( 'desc' )
      .draw();
}

function articlesTable(){
  var table = $('#articles').DataTable();
 
  // table
  //     .column( '0:visible' )
  //     .order( 'desc' )
  //     .draw();
}

function sourceDetailTable() {
  var path = '../../data/source/'.concat('@sourceId')
  $('#sources').DataTable( {
    "ajax": path,
    "columns": [
      { "data": "Title" },
      { "data": "Author" },
      { "data": "Description" },
      { "data": "Sentiment" }
    ]
  } );
}
