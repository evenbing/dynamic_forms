//Load existing fields from database and show in drop zones
$(document).ready(function () {

    $(".dragzones").draggable({
        start: handleDragStart,
        cursor: 'move',
        revert: "invalid",
        helper: 'clone',
    });
    $(".dropzones").droppable({
        drop: handleDropEvent,
        tolerance: "touch",


    });

    updateDynamicFields(formID);
    loadDynamicFields(formID);
});



function handleDragStart(event, ui) { }

function handleDropEvent(event, ui) {
    if (ui.draggable.element !== undefined) {
        ui.draggable.element.droppable('enable');
    }

    if ($(this).find(".dynamicfield").length > 0) { return; }

    ui.draggable.draggable('option', 'revert', "invalid");
    ui.draggable.element = $(this);

    var fieldName = ui.draggable.data('field-name');

    if (fieldName && fieldName.length) {
        createField(fieldName, this.id);   
    }
}


var createField = function (fieldName, parentDropZoneID) {
    return renderField(-1, fieldName, {}, parentDropZoneID);
};


var renderField = function (id, name, state, parentDropZoneID) {
    $.ajax({
        type: 'POST',
        url: renderFieldUrl,
        data: { name: name, state: state, parentDropZoneID: parentDropZoneID, __RequestVerificationToken: requestAntiForgeryToken },
        async: false,
        success: function (data) {
            var dom = $($.parseHTML($.trim(data)));
            if (dom == null) {
                return null;
            }
            dom.addClass('dynamicfield-dropped');

            dom.attr("data-clientid", "dynamicfield_" + fieldClientID);        
            dom.children().first().attr("data-clientid", "dynamicfield_" + fieldClientID);
            fieldClientID++;

          

            var dropzoneID = '#' + parentDropZoneID;
            var editor = $(dropzoneID);            
            editor.append(dom);
           
            var elt = dom.get(0);

            elt.viewModel = {
                name: name,
                state: state,
                parentDropZoneID: parentDropZoneID,
                clientId: dom.attr("data-clientid"),
                hasForm: dynamicFields[name].hasForm
            };
            
            
            saveLocal(formID);

            if (dynamicFields[name].hasForm) {
                var edit = function () {
                    //saveLocal(formID);
                    //window.location.href = editFieldUrl + "/" + $("#id").val() + "?name=" + name + "&clientId=" + elt.viewModel.clientId + "&localId=" + localId;
                    window.location.href = editFieldUrl + "?FieldName=" + name + "&ClientId=" + elt.viewModel.clientId + "&FormId=" + formID + "&returnUrl=" + returnUrl;
                    //http://localhost:5400/Admin/DM.Orchard.DynamicForms/EditField?FieldName=DynamicTextField
                };

                //Make active form to non editable
                if (!editFlag) {
                    dom.dblclick(edit);
                    
                }
                elt.viewModel.edit = edit;            
            }

            dom.on("click", function () {
                if (!editFlag) {
                    var self = $(this);
                    var toolbar = $('#dynamicfield-toolbar');

                    refreshToolbar(this, toolbar);

                    toolbar.position({
                        my: "right bottom",
                        at: "middle top",
                        offset: "0 -5",
                        of: self,
                        collision: "none"
                    });

                    toolbar.get(0).target = this;
                    toolbar.show();

                    return false;
                }
            });
        }
    });
};


var refreshToolbar = function (target, toolbar) {
    target = $(target);
    toolbar = $(toolbar);
    var deleteButton = $('#dynamicfield-toolbar-delete');
    deleteButton.unbind("click").click(function () {
        
        target.parent().addClass('ui-droppable');
        target.parent().droppable({
            drop: handleDropEvent,
            tolerance: "touch",
        });
        target.remove();        
        toolbar.hide();
    });
}

var AddNewDropZone = function () {
    var zoneID = 'dropzone' + zoneCount;

    $('#dynamicfield-editor').append("<div class='dropzones' id='" + zoneID + "'></div><br />");
   
    $('#' +zoneID).droppable({
        drop: handleDropEvent,
        tolerance: "touch",
    });
    zoneCount++;
}



//-----------------------------------
//Drag drop of fields should render in drop zones

//Double click to edit field and on Save, the field should appear in drop zone

