mergeInto(LibraryManager.library, {
    CopyToClipboardWebGL: function(text) {
        var str = UTF8ToString(text);

        // Fallback approach that works better in iframes
        var textArea = document.createElement("textarea");
        textArea.value = str;
        // Make the textarea visible but tiny
        textArea.style.position = "fixed";
        textArea.style.opacity = 0;
        textArea.style.left = "0";
        textArea.style.top = "0";
        textArea.style.width = "2em";
        textArea.style.height = "2em";
        textArea.style.padding = 0;
        textArea.style.border = "none";
        textArea.style.outline = "none";
        textArea.style.boxShadow = "none";
        textArea.style.background = "transparent";

        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();

        try {
            var successful = document.execCommand('copy');
            if (!successful) {
                console.log('Failed to copy text');
            }
        } catch (err) {
            console.error('Failed to copy text: ', err);
        }

        document.body.removeChild(textArea);
    }
});