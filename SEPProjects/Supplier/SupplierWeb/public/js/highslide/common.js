       	// remove the registerOverlay call to disable the controlbar
	    hs.registerOverlay(
    	    {
    		    thumbnailId: null,
    		    overlayId: 'controlbar',
    		    position: 'top right',
    		    hideOnMouseOut: true
		    }
	    );
	
        hs.graphicsDir = '/js/highslide/graphics/';
        
        // Identify a caption for all images. This can also be set inline for each image.
        hs.captionId = 'the-caption';
        
        hs.outlineType = 'rounded-white';
        
        hs.outlineWhileAnimating = true;
        
