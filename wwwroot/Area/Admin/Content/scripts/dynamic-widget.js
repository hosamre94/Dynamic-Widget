import {setupWebpageUrlSelector} from "./webpage-url-selector";

let _isFirstTime;
let _cachedTemplates = {};

export function initiateDynamicWidget(){
    _isFirstTime = true;
    formSubmit();
    refreshUI();
    dragAndDrop();
}

function formSubmit() {
    $('[data-dynamic-input]').closest('form').on('submit', function () {
        let $form = $('<form>');
        $('[data-dynamic-input]').map((x, el) => {
            $('<input>').attr({
                type: 'hidden',
                name: el.name,
                value: el.type === 'checkbox' ? el.checked : el.value
            }).appendTo($form);
            //Maybe remove the element from the dom or keep it if the submit is faild for some reasons
            //$(el).remove()
        })

        let obj = $form.serializeJSON()
        $('<input>').attr({
            type: 'hidden',
            name: 'Properties',
            value: JSON.stringify(obj)
        }).appendTo($(this));
    })
}

function refreshUI() {
    $(".dynamic-widget-array-container .add-row").each(function (i) {
        let arrayContainer = $(this).closest('.dynamic-widget-array-container');
        
        if (_isFirstTime) {
            const _cardTemplate = $(arrayContainer.children('.card')[0]).clone();
            const _arrayId = $(this).data('array-id');
            
            _cardTemplate.find('.media-selector-initialized').each(function(index){
                let $media = $(this);
                $media.off();
                $media.removeClass('media-selector-initialized');
                $media.attr('id', `m_temp_${index}`)
                $media.closest('.form-group').html($media);
            });

            _cardTemplate.find('input,select').val('');
            _cachedTemplates[_arrayId] = _cardTemplate;
        }

        //To make sure the event is not duplicated and we have a backup row if all rows are deleted
        $(this).off().click(function () {
            const _clone = _cachedTemplates[$(this).data('array-id')].clone();
            const _size = $(this).closest('.dynamic-widget-array-container').children('.card').length;

            _clone.find('[data-dynamic-input]').each(function (){
                $(this).attr('id', $(this).attr('id') + '_' + _size )
            });

            //find a better way to integrate the custom inputs
            const _uniqueInputs = _clone.find('[data-unique-key]');
            if (_uniqueInputs.length > 0) {
                _uniqueInputs.each(function () {
                    const _id = "uk_" + Math.random().toString(36).substr(2, 5);
                    $(this).val(_id);
                    $(this).next().html(_id);
                })
            }
            _clone.find('[data-type=media-selector], [class=media-selector]').mediaSelector();
            _clone.find('.rowIndex').html(_size + 1);
            $(this).closest('.dynamic-widget-array-container').children(':last-child').before(_clone);
            setupWebpageUrlSelector();
            refreshUI();
        });
    });

    $(".dynamic-widget-array-container .delete-row").off().click(function () {
        const didConfirm = confirm("Are you sure You want to delete");
        if (didConfirm === true) {
            $(this).closest('.card').remove();
            resetArrayIndexes();
        }
    });

    $(".dynamic-widget-array-container").each((_, element) => {
        const $element = $(element);
        $element.sortable({
            // handle: ".sort-row",
            items: "> .card",
            update: function (event, ui) {
                resetArrayIndexes();
            }
        });
    });
    _isFirstTime = false;
}

function resetArrayIndexes(){
    $('.dynamic-widget-array-container').each(function(){
       let arrayParent= $(this);
       let index = 0;
       arrayParent.children('.card').each(function (){
          let cardItem = $(this);
          cardItem.find('.rowIndex').html(++index);
       });
    });
}

function dragAndDrop() {
    if (document.querySelectorAll("#dynamic-widget-drag").length)
    {
        document.querySelector("#dynamic-widget-drag").ondragstart = function (e) {
            const dataTransfer = e.dataTransfer;
            dataTransfer.effectAllowed
            dataTransfer.setData("Text", e.target.dataset.text);
        }   
    }
}