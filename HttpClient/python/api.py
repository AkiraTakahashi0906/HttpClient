from flask import Flask,jsonify
from flask import Flask, render_template, request #追加
app = Flask(__name__)

@app.route('/')
def hello_world():
    #return 'Hello, World!'
    return jsonify([{"language":"太郎1","language2":"M"},{"language":"太郎2","language2":"M"}])

@app.route('/hello/<name>')
def hello(name=None):
    #return name
    return jsonify([{"language":name,"language2":"M"},{"language":"太郎2","language2":"M"}])
    #return render_template('hello.html', title='flask test', name=name) 

@app.route('/hello', methods=['POST']) #Methodを明示する必要あり
def hello_post():
    if request.method == 'POST':
        name = request.json['language']
        name2 = request.json['language2']
    else:
        name = "no name."
        name2 = "no name2."
    #return render_template('hello.html', title='flask test', name=name) 
    return jsonify({"language":name,"language2":name2})
    


if __name__ == '__main__':
    app.run(debug=False, host='127.0.0.1', port=8888)