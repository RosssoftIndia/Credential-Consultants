/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    // The toolbar groups arrangement, optimized for two toolbar rows.
config.toolbar_Full =
[
	['Preview', 'Cut', 'Copy', 'Paste', 'PasteFromWord', '-', 'Print', 'SpellChecker'],
	['Undo', 'Redo', '-', 'RemoveFormat'],
	['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
	['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
	['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
	['Link', 'Unlink', 'Anchor']
];

config.toolbar_Basic =
[
['Preview', 'Cut', 'Copy', 'Paste', 'PasteFromWord', 'RemoveFormat']
];
    config.removePlugins = 'elementspath';
    config.theme = 'default';
    config.skin = 'v2';
};
