// Write your Javascript code.
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
  var path = '../data/trendingtopics/';
  var table = $('#topics').DataTable({
    "ajax": path,
    "columnDefs": [ 
      {
        "targets":0,
        'responsivePriority': 5
      },
      {
      "targets": 1,
      "data": null,
      "responsivePriority": 1,
      "render": function ( data, type, full, meta ) {
        return '<a href="topics/detail/'+full.id+'">' + data + '</a>';
      }
    },
    {
      "targets": 2,
      "data": null,
      "responsivePriority": 2,
      "render": function ( data, type, full, meta ) {
        return (data*100).toFixed(1).toString() + '%';
      }
    }
    ],
    "columns": [
      { "data": "score" },
      { "data": "keyPhrase"},
      { "data": "averageSentiment" }
    ]
  });
 
  table
      .column( '0:visible' )
      .order( 'desc' )
      .draw();
}

function allSourcesTable(){
  var path = '../data/allsources'
  $('#sources').DataTable( {
    "ajax": path,
    "columnDefs": [ {
      "targets": 0,
      "responsivePriority": 1,
      "data": null,
      "render": function ( data, type, full, meta ) {
        return '<a href="sources/detail/'+full.Id+'">' + data + '</a>';
      }
    }],
    "columns": [
      { "data": "Name" },
      { "data": "Description" }
    ]
  } );
} 

function articlesTable(){
  var table = $('#articles').DataTable();
 
  // table
  //     .column( '0:visible' )
  //     .order( 'desc' )
  //     .draw();
}

function sourceDetailTable(sourceId) {
  var path = '../../data/source/'.concat(sourceId);
  $('#sources').DataTable( {
    "ajax": path,
    "columnDefs": [
      {
        "targets":0,
        "responsivePriority": 1
      },
      {
        "targets":1,
        "responsivePriority": 3
      },
      {
      "targets": 3,
      "responsivePriority": 2,
      "data": null,
      "render": function ( data, type, full, meta ) {
        return (data*100).toFixed(1).toString() + '%';
      }
    }
    ],
    "columns": [
      { "data": "Title" },
      { "data": "Author" },
      { "data": "Description" },
      { "data": "Sentiment" }
    ]
  } );
}

function loadTrendPlot(id) {
    var url = "../../Data/Trend/" + id

    function loadData(dates, nums, sentiment)
    {
        var data = [
            {
                x: dates,
                y: nums,
                name: 'Count',
                type: 'scatter'
            },
            {
                x: dates,
                y: sentiment,
                name: 'Sentiment',
                yaxis: 'y2',
                type: 'scatter'
            }
    ];

    var layout = {
        title: id,
        xaxis: {
            title: 'Date',
            showgrid: true,
            zeroline: false
        },
        yaxis: {
            title: 'Reference Count',
            showline: false
            },
        yaxis2: {
            title: 'Sentiment',
            titlefont: {color: 'rgb(148, 103, 189)'},
            tickfont: {color: 'rgb(148, 103, 189)'},
            overlaying: 'y',
            side: 'right'
        }
        };

    Plotly.newPlot('myDiv', data, layout);
    $( ".loader" ).remove();
    }

    var xvals = []
    var yvals = []
    var sentiment = [];

    $.ajax({ url: url,
            context: document.body,
            success: function(result){
                result.data.forEach(function callback(val, index, array) {
                    xvals.push(val.Day)
                    yvals.push(val.Num)
                    sentiment.push(val.AverageSentiment)
                });
                loadData(xvals, yvals, sentiment);
            }});
    
}