var page = require('webpage').create();
page.open('https://google.com', function () {
    console.log(page.content);
    phantom.exit();
});