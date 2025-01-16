import React, {useRef, useState} from "react";
import {useDrag} from "react-dnd";

interface DragnDropCardProps {
    id: string;
    title: string;
    description: string;
}

const DragnDropCard: React.FC<DragnDropCardProps> = ({id, title, description}) => {
    const modalRef = useRef<HTMLDialogElement>(null);
    const [cardTitle, setCardTitle] = useState(title);
    const [cardDescription, setCardDescription] = useState(description);

    const [{isDragging}, drag] = useDrag(() => ({
        type: "CARD",
        item: {id, title: cardTitle, description: cardDescription},
        collect: (monitor) => ({
            isDragging: monitor.isDragging(),
        }),
    }));

    const editCard = () => {
        if (modalRef.current) {
            modalRef.current.showModal();
        }
    };

    const saveCard = () => {
        if (modalRef.current) {
            modalRef.current.close();
        }
    };

    return (
        <div>
            <div ref={drag} style={{border: isDragging ? "5px solid orange" : "0px"}}>
                <div className="card-body bg-neutral">
                    <div className="card-actions justify-end">
                        <button onClick={editCard} className="btn btn-square btn-xs">
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                height="20px"
                                viewBox="0 -960 960 960"
                                width="20px"
                                className="fill-base-content"
                            >
                                <path
                                    d="M200-200h57l391-391-57-57-391 391v57Zm-80 80v-170l528-527q12-11 26.5-17t30.5-6q16 0 31 6t26 18l55 56q12 11 17.5 26t5.5 30q0 16-5.5 30.5T817-647L290-120H120Zm640-584-56-56 56 56Zm-141 85-28-29 57 57-29-28Z"/>
                            </svg>
                        </button>
                    </div>
                    <h2 className="card-title text-neutral-content">{cardTitle}</h2>
                    <p className="text-neutral-content">{cardDescription}</p>
                </div>
            </div>
            <dialog ref={modalRef} className="modal">
                <div className="modal-box">
                    <h3 className="font-bold text-lg">Edit card!</h3>
                    <div className="modal-body flex-col space-y-6">
                        <div className="flex content-center justify-between">
                            <p className="py-3 pr-4 text-lg">Title:</p>
                            <input
                                type="text"
                                placeholder="Type here"
                                value={cardTitle}
                                onChange={(e) => setCardTitle(e.target.value)}
                                className="input input-bordered w-full max-w-xs"
                            />
                        </div>
                        <div className="flex content-center justify-between">
                            <p className="py-3 pr-4 text-lg">Description:</p>
                            <textarea
                                className="textarea textarea-bordered w-full max-w-xs"
                                value={cardDescription}
                                onChange={(e) => setCardDescription(e.target.value)}
                                placeholder="Description"
                            />
                        </div>
                    </div>
                    <div className="modal-action">
                        <button
                            className="btn btn-warning-content"
                            onClick={() => modalRef.current?.close()}
                        >
                            Cancel
                        </button>
                        <button className="btn btn-primary" onClick={saveCard}>
                            Save
                        </button>
                    </div>
                </div>
            </dialog>
        </div>
    );
};

export default DragnDropCard;
