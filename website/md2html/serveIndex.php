<?

// .htaccess redirected a request to a folder containing index.md
$md2htmlFolder = '/home/customer/www/swharden.com/gitRepos/md2html-php/md2html';
require($md2htmlFolder . '/md2html.php');
$mdFilePath = realpath($_SERVER['DOCUMENT_ROOT']) . rtrim($_SERVER['REQUEST_URI'], '/') . '/index.md';
$pageTemplate = file_get_contents('page.html');
$articleTemplate = file_get_contents('article.html');
ServeSingleMarkdownFile($mdFilePath, $pageTemplate, $articleTemplate);