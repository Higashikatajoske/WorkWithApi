const baseUrl = 'http://localhost:5000';
const publicKey = '38cd79b5f2b2486d86f562e3c43034f8';
const privateKey = '8e49ff607b1f46e1a5e8f6ad5d312a80';
const requestTokenUrl = 'http://api.pixlpark.com/oauth/requesttoken/';
const accessTokenUrl = 'http://api.pixlpark.com/oauth/accesstoken/';


async function getResponse() {

    let headers = new Headers();
    headers.append('Content-Type', 'text/html');
    let response = await fetch('http://api.pixlpark.com/oauth/requesttoken?',
        {
            mode: 'no-cors',
            headers: {
                'Access-Control-Allow-Origin': '*',

            }
        })

    if (response.ok) {
        console.log("ok");
    }
    else
        console.log("not ok");


    await console.log(response);
    await console.log(response.status);

}

getResponse();

const androidGitHubUrl = 'https://api.github.com/search/repositories?q=android';;

/*const xhr = new XMLHttpRequest();
xhr.open('GET', requestTokenUrl);
xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
xhr.send();*/



// читаем ответ в формате JSON