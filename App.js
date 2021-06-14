var mouseX;
var mouseY;
var gamePiece;

var gameArea = {
    canvas: document.createElement("canvas"),
    start: function() {
        this.canvas.width = 200;
        this.canvas.height = 200;
        this.canvas.addEventListener('mousedown', function(e){
            getCursorPosition(this.canvas, e);
        });
        this.context = this.canvas.getContext("2d");
        document.body.insertBefore(this.canvas, document.body.childNodes[0]);
        this.interval = setInterval(updateGameArea, 20);
    },
    clear : function() {
        this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
    }
}

function startGame() {
    gameArea.start();
    gamePiece = new component(1,1,"red",10,10);
}

function getCursorPosition(canvas, event) {
    const rect = canvas.getBoundingClientRect();
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;
    console.log("x: " + x + " y: " + y);
}

function component(width, height, color, x, y) {
    this.width = width;
    this.height = height;
    this.x = x;
    this.y = y;
    
    this.update = function(){
        ctx = gameArea.context;
        ctx.fillStyle = color;
        ctx.fillRect(this.x, this.y, this.width, this.height);
      }
}

function updateGameArea() {
    gameArea.clear();
    if(gamePiece.y < gameArea.canvas.height - 1)
    {
        gamePiece.y += 1;
    }
    gamePiece.update();
  }