import React, { useState } from 'react';
import './App.css';

export default function SudokuGrid() {
    const [grid, setGrid] = useState(Array(9).fill(".").map(() => Array(9).fill('.')));
    const [output, setOutput] = useState('');

    function handleChange(row, col, value){
        if (value >= 1 && value <= 9) {
            // #region Expanded version of below code
            //setGrid(prev => {
            //    const newGrid = [];
            //    for (let i = 0; i < prev.length; i++) {
            //        const rowCopy = [];
            //        for (let j = 0; j < prev[i].length; j++) {
            //            rowCopy.push(prev[i][j]);
            //        }
            //        newGrid.push(rowCopy);
            //    }
            //    newGrid[row][col] = value;
            //    return newGrid;
            //});
            // #endregion
            setGrid(prev => {                    
                const newGrid = prev.map(r => [...r]); 
                newGrid[row][col] = value;              
                return newGrid;                          
            });
        }
        else if (value === '') {
            setGrid(prev => {
                const newGrid = prev.map(r => [...r]);
                newGrid[row][col] = '.';
                return newGrid;
            });
        }
    };
    
    async function handleSubmit() {
        try {
            const response = await fetch('http://localhost:5078/SudokuValidation/ValidateSudokuBoard', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(grid)
            });
            if (response.ok) {
                setOutput(await response.json());
            }
            else {
                throw new Error(`Response status: ${response.status}`);
            }
        } catch (error) {
            alert('Error submitting Sudoku board: ' + error.message);
        }
    };

    return (
        <div className="sudoku-container">
            <table className="sudoku-grid">
                <tbody>
                    {grid.map((row, rowIndex) => (
                        <tr key={rowIndex}>
                            {row.map((cell, colIndex) => (
                                <td key={`${rowIndex}-${colIndex}`}>
                                    <input
                                        type="text"
                                        maxLength={1}
                                        value={cell === '.' ? '' : cell}
                                        placeholder="."
                                        onChange={e => handleChange(rowIndex, colIndex, e.target.value)}
                                        className="sudoku-cell"
                                    />
                                </td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>

            <button onClick={() => handleSubmit()} className="sudoku-input">
                Validate Sudoku Board
            </button>

            <div className="sudoku-output">
                <h3>Output: {String(output)}</h3>
            </div>
        </div>
    );
};